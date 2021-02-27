using Business.Abstract;
using Business.BusinessAspect.Autofac;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Validation;
using Core.CrossCuttingConcerns.Validation;
using Core.Utilities.Business;
using Core.Utilities.Results;
using DataAccess.Abstract;
using DataAccess.Concrete.InMemory;
using Entities.Concrete;
using Entities.DTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Business.Concrete
{
    public class ProductManager : IProductService
    {
        IProductDal _productDal;
        ICategoryService _categoryService;
        public ProductManager(IProductDal productDal,ICategoryService categoryService)
        {
            _productDal = productDal;
            _categoryService = categoryService;
        }

        [SecuredOperation("product.add,admin")]

        [ValidationAspect(typeof(ProductValidator))]
        public IResult Add(Product product)
        {
            // (3) eklenmiş hali
            IResult result = BusinessRules.Run(CheckIfProductNameExists(product.ProductName), CheckIfProductCountOfCategoryCorrect(product.CategoryId), CheckIfCategoryLimitExceded());
            if (result != null)
            {
                return result;
            }
            _productDal.Add(product);
            return new SuccessResult(Messages.ProductAdded);


            // Business codes:
            // ********************************* Sorular ****************************************
            //// (1) Bir kategoride en fazla 10 ürün olabilir

            //// (2) Aynı isimde ürün eklenemez

            //// (3) Eğer mevcut kategori sayısı 15' i geçtiyse sisteme yeni ürün eklenemez.

            // ********************************* Cevaplar ****************************************

            //// (1) Bir kategoride en fazla 10 ürün olabilir
            //var result = _productDal.GetAll(p => p.CategoryId == product.CategoryId).Count;
            //if (result>=10)  //ilerde 15 olursa Update de değiştirme unutulursa açık oluşur.
            //{
            //    return new ErrorResult(Messages.ProductCountOfCategoryError);
            //}

            //// (1) Bir kategoride en fazla 10 ürün olabilir - iyileştirilmiş kod
            //if (CheckIfProductCountOfCategoryCorrect(product.CategoryId).Success)
            //{
            //    //// (2) Aynı isimde ürün eklenemez - sonradan eklendi
            //    if (CheckIfProductNameExists(product.ProductName).Success)
            //    {
            //        _productDal.Add(product);
            //        return new SuccessResult(Messages.ProductAdded);
            //    }              
            //}
            //return new ErrorResult();

            //// (1) ve (2) nin iyileştirilmiş hali
            //// Core.Utilities.Business --> BusinessRules.cs oluşturulur.
            //IResult result = BusinessRules.Run(CheckIfProductNameExists(product.ProductName), CheckIfProductCountOfCategoryCorrect(product.CategoryId));
            //if (result != null)
            //{
            //    return result;
            //}
            //_productDal.Add(product);
            //return new SuccessResult(Messages.ProductAdded);


            //*********************************************************************************
            ////validation :  (ürünün ismi 2 harfli olamaz gibi kodlar)
            //var context = new ValidationContext<Product>(product);
            //ProductValidator productValidator = new ProductValidator();
            //var result = productValidator.Validate(context);
            //if (!result.IsValid)
            //{
            //    throw new ValidationException(result.Errors);
            //}

            //ValidationTool.Validate(new ProductValidator(),product);    //iş kuralı değil

            //_productDal.Add(product);

            //return new SuccessResult(Messages.ProductAdded);

            //**********************************************************************************
        }

        //[ValidationAspect(typeof(ProductValidator))]
        public IResult Update(Product product)
        {          
            //// (1) Bir kategoride en fazla 10 ürün olabilir
            //var result = _productDal.GetAll(p => p.CategoryId == product.CategoryId).Count;
            //if (result >= 10)
            //{
            //    return new ErrorResult(Messages.ProductCountOfCategoryError);
            //}          

            _productDal.Update(product);

            return new SuccessResult(Messages.ProductUpdated);
        }

        public IDataResult<List<Product>> GetAll()
        {
            //if (DateTime.Now.Hour == 1)
            //{
            //    return new ErrorDataResult<List<Product>>(Messages.MaintenanceTime);
            //}

            return new SuccessDataResult<List<Product>>(_productDal.GetAll(), Messages.ProductsListed);
        }

        public IDataResult<Product> GetById(int productId)
        {
            return new SuccessDataResult<Product>(_productDal.Get(p => p.ProductId == productId));
        }

        public IDataResult<List<ProductDetailDto>> GetProductDetails()
        {
            if (DateTime.Now.Hour == 23)
            {
                return new ErrorDataResult<List<ProductDetailDto>>(Messages.MaintenanceTime);
            }
            return new SuccessDataResult<List<ProductDetailDto>>(_productDal.GetProductDetails());
        }

        public IDataResult<List<Product>> GetAllByCategoryId(int id)
        {
            return new SuccessDataResult<List<Product>>(_productDal.GetAll(p => p.CategoryId == id));
        }

        public IDataResult<List<Product>> GetByUnitPrice(decimal min, decimal max)
        {
            return new SuccessDataResult<List<Product>>(_productDal.GetAll(p => p.UnitPrice >= min && p.UnitPrice <= max));
        }

        // (1) iyileştirilmiş kod
        private IResult CheckIfProductCountOfCategoryCorrect(int categoryId)
        {
            //Select count(*) from product where categoryId=1

            var result = _productDal.GetAll(p => p.CategoryId == categoryId).Count;
            if (result >= 15)
            {
                return new ErrorResult(Messages.ProductCountOfCategoryError);
            }
            return new SuccessResult();
        }

        // (2)
        private IResult CheckIfProductNameExists(string productName)
        {
            var result = _productDal.GetAll(p => p.ProductName == productName).Any();
            if (result)
            {
                return new ErrorResult(Messages.ProductNameAlreadyExists);
            }
            return new SuccessResult();
        }

        // (3)
        private IResult CheckIfCategoryLimitExceded()
        {
            var result = _categoryService.GetAll();
            if (result.Data.Count>15)
            {
                return new ErrorResult(Messages.CategoryLimitExceded);
            }

            return new SuccessResult();
        }
    }
}