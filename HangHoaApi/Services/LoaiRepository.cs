using HangHoaApi.Entity;
using HangHoaApi.Models;
using System.Collections.Generic;
using System.Linq;

namespace HangHoaApi.Services
{
    public class LoaiRepository : ILoaiRepository
    {
        private readonly MyDbContext _context;

        public LoaiRepository(MyDbContext context)
        {
            _context = context;
        }
        public LoaiModels Add(LoaiModels2 loaiModels2)
        {
            var loaiMd = new LoaiEntity
            {
                TenLoai = loaiModels2.TenLoai,
                
            };
            _context.Add(loaiMd);
            _context.SaveChanges();
            return new LoaiModels
            {
                MaLoai = loaiMd.MaLoai,
                TenLoai = loaiMd.TenLoai
            };
        }

        public void Delete(int id)
        {
            var loaiModels = _context.loaiEntities.SingleOrDefault(op => op.MaLoai == id);
            if(loaiModels != null)
            {
                _context.Remove(loaiModels);
                _context.SaveChanges();               
            }       
        }

        public List<LoaiModels> GetAll()
        {
            var loaiModels = _context.loaiEntities.Select(lo => new LoaiModels
            {
                MaLoai = lo.MaLoai,
                TenLoai = lo.TenLoai,
            }) ;
            return loaiModels.ToList();
        }

        public List<LoaiModels> GetAllLoai(string search, string sortBy)
        {
            var allLoai = _context.loaiEntities.AsQueryable();
            if (!string.IsNullOrEmpty(search))
            {
                allLoai = allLoai.Where(lo => lo.TenLoai.Contains(search));
            }
            // sap xep ten hang hoa tawng dan
            allLoai = allLoai.OrderBy(lo => lo.TenLoai);
            if (!string.IsNullOrEmpty(sortBy))
            {
                switch (sortBy)
                {
                    case "TenLoai": allLoai = allLoai.OrderBy(lo => lo.TenLoai);
                        break;
                    case "MaLoai": allLoai = allLoai.OrderByDescending(lo => lo.MaLoai);
                        break;
                }
            }

            var loaiModels = allLoai.Select(lo => new LoaiModels
            {
                MaLoai = lo.MaLoai,
                TenLoai = lo.TenLoai,
            });
            return loaiModels.ToList();
        }

        public LoaiModels GetById(int id)
        {
            var loaiModels = _context.loaiEntities.SingleOrDefault(op => op.MaLoai == id);
            if(loaiModels != null)
            {
                return new LoaiModels
                {
                    MaLoai = loaiModels.MaLoai,
                    TenLoai = loaiModels.TenLoai
                };    
            }
            return null;
        }

        public void Upate(LoaiModels loaiModels)
        {
            var _loaiModels = _context.loaiEntities.SingleOrDefault(op => op.MaLoai == loaiModels.MaLoai);
            if(_loaiModels != null)
            {
                _loaiModels.TenLoai = loaiModels.TenLoai;
                _context.SaveChanges();
            }
        }
    }
}
