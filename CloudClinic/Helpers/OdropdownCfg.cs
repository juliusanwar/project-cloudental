namespace CloudClinic.Helpers
{
    /// <summary>
    /// Odropdown config
    /// </summary>
    public class OdropdownCfg
    {
        private readonly OdropdownTag tag = new OdropdownTag();

        /// <summary>
        /// label text in front of the caption/selected item text
        /// </summary>
        /// <param name="o"></param>
        /// <returns></returns>
        public OdropdownCfg InLabel(string o)
        {
            tag.InLabel = o;
            return this;
        }

        /// <summary>
        /// caption when no item is selected
        /// </summary>
        /// <param name="o"></param>
        /// <returns></returns>
        public OdropdownCfg Caption(string o)
        {
            tag.Caption = o;
            return this;
        }

        /// <summary>
        /// autoselect first item on load when no value is matched (change will be triggered)
        /// </summary>
        /// <returns></returns>
        public OdropdownCfg AutoSelectFirst()
        {
            tag.AutoSelectFirst = true;
            return this;
        }

        /// <summary>
        /// don't close dropdown on item select
        /// </summary>
        /// <returns></returns>
        public OdropdownCfg NoSelectClose()
        {
            tag.NoSelClose = true;
            return this;
        }

        internal OdropdownTag ToTag()
        {
            return tag;
        }
    }
}