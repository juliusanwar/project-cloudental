namespace CloudClinic.ViewModels.Input
{
    public class LookupDemoCfgInput
    {
        public int? PasienId { get; set; }

        public bool ClearButton { get; set; }

        public bool HighlightChange { get; set; }

        public int Height { get; set; }

        public int Width { get; set; }

        public bool Fullscreen { get; set; }
    }
}