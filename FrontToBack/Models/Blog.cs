namespace FrontToBack.Models
{
    public class Blog
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Desc { get; set; }
        public string ImageUrl { get; set; }
        public DateTime CreatedDate { get; set; }

        //public override string ToString()
        //{
        //    return Name; // artiq bu metod type deyil de adi qaytaracaq
        //}
    }
}
