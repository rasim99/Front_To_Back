using FrontToBack.Models;

namespace FrontToBack.ViewModels
{
    public class HomeVM
    {
        public List<Slider> Sliders { get; set; }
        public SliderContent SliderContent { get; set; }
        public List<Category> Categories { get; set; }
        public List<Product> Products { get; set; }
        public List<Blog> Blogs { get; set; }
        public Expert Expert { get; set; }
        public List<ExpertContent> ExpertContents { get; set; }
        public List<Say> Says { get; set; }
        public List<Instagram> Instagrams { get; set; }

    }
}
