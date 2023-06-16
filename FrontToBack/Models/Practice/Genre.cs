namespace FrontToBack.Models.Practice
{
    public class Genre
    {
        public int Id { get; set; }
        public string Name { get; set; }
        List<BookGenre> BookGenres { get; set; }
    }
}
