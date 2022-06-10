namespace HangHoaApi.Entity
{
    public class HangHoaEntity
    {
        public int MaHangHoa { get; set; }
        public string TenHangHoa { get; set; }
        public string MoTa { get; set; }
        public double DonGia { get; set; }
        public byte GiamGia { get; set; }
        public int? MaLoai { get; set; }
        public LoaiEntity LoaiEntity { get; set; }
    }
}
