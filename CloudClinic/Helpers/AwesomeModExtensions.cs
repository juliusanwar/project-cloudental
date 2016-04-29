using System;
using System.Collections.Generic;
using System.Threading;

using Omu.AwesomeMvc;

namespace CloudClinic.Helpers
{
    /// <summary>
    /// ASP.net MVC Awesome mod extensions
    /// </summary>
    public static class AwesomeModExtensions
    {
        public static AjaxRadioList<T> Odropdown<T>(this AjaxRadioList<T> arl, Action<OdropdownCfg> setCfg = null)
        {
            var res = arl.Mod("awem.odropdown");
            var odcfg = new OdropdownCfg();

            if (setCfg != null)
            {
                setCfg(odcfg);
                res.Tag(odcfg.ToTag());
            }

            return res;
        }

        public static AjaxRadioList<T> MenuDropdown<T>(this AjaxRadioList<T> arl, Action<OdropdownCfg> setCfg = null)
        {
            var res = arl.Mod("awem.menuDropdown");
            var odcfg = new OdropdownCfg();

            if (setCfg != null)
            {
                setCfg(odcfg);
                res.Tag(odcfg.ToTag());
            }

            return res;
        }

        public static AjaxRadioList<T> ColorDropdown<T>(this AjaxRadioList<T> arl, Action<OdropdownCfg> setCfg = null)
        {
            arl.Mod("awem.colorDropdown");
            var odcfg = new OdropdownCfg();

            if (setCfg != null)
            {
                setCfg(odcfg);
                arl.Tag(odcfg.ToTag());
            }

            return arl;
        }

        public static AjaxRadioList<T> Combobox<T>(this AjaxRadioList<T> arl, Action<OdropdownCfg> setCfg = null)
        {
            arl.Mod("awem.combobox");
            var odcfg = new OdropdownCfg();

            if (setCfg != null)
            {
                setCfg(odcfg);
                arl.Tag(odcfg.ToTag());
            }

            return arl;
        }

        public static AjaxRadioList<T> TimePicker<T>(this AjaxRadioList<T> arl, Action<TimePickerCfg> setCfg = null)
        {
            arl.Mod("awem.timepicker");
            arl.UnobsValid(false);

            var cfg = new TimePickerCfg();
            var tag = new TimePickerTag();

            if (setCfg != null)
            {
                setCfg(cfg);
                tag = cfg.ToTag();
            }

            var cformat = Thread.CurrentThread.CurrentCulture.DateTimeFormat;
            var isAmPm = cformat.ShortTimePattern.Contains("h");

            if (isAmPm)
            {
                tag.AmPm = new[] { cformat.AMDesignator, cformat.PMDesignator };
            }

            arl.Tag(tag);

            arl.ValueRenderer(
                o =>
                {
                    if (o != null)
                    {
                        if (o is DateTime)
                        {
                            return ((DateTime)o).ToString(cformat.ShortTimePattern);
                        }

                        return o.ToString();
                    }

                    return string.Empty;
                });
            return arl;
        }

        public static AjaxRadioList<T> ImgDropdown<T>(this AjaxRadioList<T> arl, Action<OdropdownCfg> setCfg = null)
        {
            arl.Mod("awem.imgDropdown");
            var odcfg = new OdropdownCfg();

            if (setCfg != null)
            {
                setCfg(odcfg);
                arl.Tag(odcfg.ToTag());
            }

            return arl;
        }

        public static AjaxRadioList<T> ButtonGroup<T>(this AjaxRadioList<T> arl, Action<OdropdownCfg> setCfg = null)
        {
            arl.Mod("awem.buttonGroupRadio");
            var odcfg = new OdropdownCfg();

            if (setCfg != null)
            {
                setCfg(odcfg);
                arl.Tag(odcfg.ToTag());
            }

            return arl;
        }

        public static AjaxCheckboxList<T> Multiselect<T>(this AjaxCheckboxList<T> arl, Action<OdropdownCfg> setCfg = null)
        {
            arl.Mod("awem.multiselect");
            var odcfg = new OdropdownCfg();

            if (setCfg != null)
            {
                setCfg(odcfg);
                arl.Tag(odcfg.ToTag());
            }

            return arl;
        }

        public static InitPopup<T> DropdownPopup<T>(this InitPopup<T> o)
        {
            o.Tag(new { DropdownPopup = true });
            return o;
        }

        public static InitPopupForm<T> DropdownPopup<T>(this InitPopupForm<T> o)
        {
            o.Tag(new { DropdownPopup = true });
            return o;
        }

        public static MultiLookup<T> DropdownPopup<T>(this MultiLookup<T> multi)
        {
            multi.Tag(new { DropdownPopup = true });
            return multi;
        }

        public static Lookup<T> DropdownPopup<T>(this Lookup<T> lookup)
        {
            lookup.Tag(new { DropdownPopup = true });
            return lookup;
        }

        public static Grid<T> Mod<T>(this Grid<T> grid, Action<GridModCfg> setCfg = null)
        {
            if (setCfg != null)
            {
                var cfg = new GridModCfg();
                setCfg(cfg);
                var info = cfg.GetInfo();
                var mods = new List<string>();
                if (info.PageSize) mods.Add("awem.gridPageSize");
                if (info.PageInfo) mods.Add("awem.gridPageInfo");
                if (info.ColumnsSelector) mods.Add("awem.gridColSel");
                if (info.InfiniteScroll) mods.Add("awem.gridInfScroll");
                if (info.AutoMiniPager) mods.Add("awem.gridAutoMiniPager");
                if (info.InlineEdit) mods.Add("awem.gridInlineEdit('" + info.CreateUrl + "','" + info.EditUrl + "')");
                grid.Mod(mods.ToArray());

                grid.BeforeRenderFuncs.Add(g =>
                            {
                                var autohide = false;
                                foreach (var column in g.GetColumns())
                                {
                                    var o = column.Tag as ColumnModTag;

                                    if (info.ColumnsAutohide)
                                    {
                                        autohide = true;

                                        if (o == null)
                                        {
                                            o = new ColumnModTag();
                                            column.Tag = o;
                                        }

                                        if (!o.Autohide.HasValue)
                                        {
                                            o.Autohide = 1;
                                        }

                                        if (o.Nohide)
                                        {
                                            o.Autohide = 0;
                                        }
                                    }
                                    else if (o != null && o.Autohide > 0)
                                    {
                                        autohide = true;
                                    }
                                }

                                if (autohide)
                                {
                                    g.GetMods().Add("awem.gridColAutohide");
                                }
                            });
            }

            return grid;
        }

        public static Column Mod(this Column column, Action<ColumnModCfg> setCfg = null)
        {
            if (setCfg != null)
            {
                var cfg = new ColumnModCfg(column);
                setCfg(cfg);
                var info = cfg.GetTag();
                column.Tag = info;
            }

            return column;
        }
    }
}