using AutoMapper;
using CouponAPI.Data;
using CouponAPI.Models;
using CouponAPI.Models.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CouponAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CouponAPIController : ControllerBase
    {
        private readonly AppDbContext _db;
        private  ResponseDTO _response;
        private readonly IMapper _mapper;
        public CouponAPIController(AppDbContext db, IMapper mapper)
        {
            _db = db;
            _response = new ResponseDTO(); 
            _mapper = mapper;   
        }
        [HttpGet]
        public ResponseDTO Get()
        {
            try
            {
                IEnumerable<Coupon> couponList = _db.Coupons.ToList();
                _response.Result = _mapper.Map<IEnumerable<CouponDTO>>(couponList);  
            }
            catch (Exception ex)
            {
                _response.Result = ex.Message;
                _response.IsSuccess = false;
            }
            return _response;
        }

        [HttpGet]
        [Route("{id:int}")]
        public ResponseDTO Get(int id)
        {
            try
            {
                Coupon coupon = _db.Coupons.FirstOrDefault(u => u.CouponId == id);
                // thay vì tạo một đối tượng và gán vào đối số thi t sử dụng AutoMapper
                //CouponDTO couponDTO = new CouponDTO()
                //{
                //    CouponId = coupon.CouponId,
                //    CouponCode = coupon.CouponCode,
                //    DiscountAmount = coupon.DiscountAmount,
                //    MinAmount = coupon.MinAmount
                //};
                _response.Result = _mapper.Map<CouponDTO>(coupon);
            }
            catch (Exception ex)
            {
                _response.Result = ex.Message;
                _response.IsSuccess = false;
            }
            return _response;
        }


        [HttpGet]
        [Route("GetByCode/{code}")]
        public ResponseDTO GetByCode(string code)
        {
            try
            {
                Coupon coupon = _db.Coupons.First(u => u.CouponCode.ToLower() == code.ToLower());
                _response.Result = _mapper.Map<CouponDTO>(coupon);
            }
            catch (Exception ex)
            {
                _response.Result = ex.Message;
                _response.IsSuccess = false;
            }
            return _response;
        }
    }
}
