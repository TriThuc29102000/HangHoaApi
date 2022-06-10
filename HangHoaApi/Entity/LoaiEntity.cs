using System.Collections;
using System.Collections.Generic;

namespace HangHoaApi.Entity
{
    public class LoaiEntity
    {
        public int MaLoai { get; set; }
        public string TenLoai { get; set; }
        public ICollection<HangHoaEntity> hangHoaEntities { get; set; }
    }
  
}
