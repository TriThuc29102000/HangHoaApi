using HangHoaApi.Models;
using System.Collections.Generic;

namespace HangHoaApi.Services
{
    public interface ILoaiRepository
    {
        public List<LoaiModels> GetAll();
        public LoaiModels GetById(int id);
        public LoaiModels Add(LoaiModels2 loaiModels2);
        public void Upate(LoaiModels loaiModels);
        public void Delete(int id);
        public List<LoaiModels> GetAllLoai(string search, string sortBy);
    }
}
