using Omu.AwesomeMvc;

namespace CloudClinic.ViewModels.Display
{
    public class FruitDisplay : KeyContent
    {
        public FruitDisplay(object key, string content, string url)
            : base(key, content)
        {
            Url = url;
        }

        public string Url { get; set; }
    }
}