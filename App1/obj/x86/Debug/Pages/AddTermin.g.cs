﻿#pragma checksum "C:\Users\K-Chan\source\repos\App1\App1\Pages\AddTermin.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "1F0377BEB6060E7E1D87DBAB505FE3B3"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace VirtualCalendarV01.Pages
{
    partial class AddTermin : 
        global::Windows.UI.Xaml.Controls.Page, 
        global::Windows.UI.Xaml.Markup.IComponentConnector,
        global::Windows.UI.Xaml.Markup.IComponentConnector2
    {
        /// <summary>
        /// Connect()
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 14.0.0.0")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public void Connect(int connectionId, object target)
        {
            switch(connectionId)
            {
            case 1:
                {
                    global::Windows.UI.Xaml.Controls.AppBarButton element1 = (global::Windows.UI.Xaml.Controls.AppBarButton)(target);
                    #line 15 "..\..\..\Pages\AddTermin.xaml"
                    ((global::Windows.UI.Xaml.Controls.AppBarButton)element1).Click += this.BackBarButton_Click;
                    #line default
                }
                break;
            case 2:
                {
                    global::Windows.UI.Xaml.Controls.AppBarButton element2 = (global::Windows.UI.Xaml.Controls.AppBarButton)(target);
                    #line 16 "..\..\..\Pages\AddTermin.xaml"
                    ((global::Windows.UI.Xaml.Controls.AppBarButton)element2).Click += this.AddBarButton_Click;
                    #line default
                }
                break;
            case 3:
                {
                    this.CbKalenderAuswahl = (global::Windows.UI.Xaml.Controls.ComboBox)(target);
                    #line 22 "..\..\..\Pages\AddTermin.xaml"
                    ((global::Windows.UI.Xaml.Controls.ComboBox)this.CbKalenderAuswahl).SelectionChanged += this.CbKalenderAuswahl_SelectionChanged;
                    #line default
                }
                break;
            case 4:
                {
                    this.Tb_Bezeichnung = (global::Windows.UI.Xaml.Controls.TextBox)(target);
                }
                break;
            case 5:
                {
                    this.CbWiederholungAuswahl = (global::Windows.UI.Xaml.Controls.ComboBox)(target);
                    #line 33 "..\..\..\Pages\AddTermin.xaml"
                    ((global::Windows.UI.Xaml.Controls.ComboBox)this.CbWiederholungAuswahl).SelectionChanged += this.CbWiederholungAuswahl_SelectionChanged;
                    #line default
                }
                break;
            case 6:
                {
                    this.Tb_TerminBeschreibung = (global::Windows.UI.Xaml.Controls.TextBox)(target);
                }
                break;
            case 7:
                {
                    this.CbPersonAuswahl = (global::Windows.UI.Xaml.Controls.ComboBox)(target);
                    #line 41 "..\..\..\Pages\AddTermin.xaml"
                    ((global::Windows.UI.Xaml.Controls.ComboBox)this.CbPersonAuswahl).SelectionChanged += this.CbPersonAuswahl_SelectionChanged;
                    #line default
                }
                break;
            case 8:
                {
                    this.EndCalendarDatePicker = (global::Windows.UI.Xaml.Controls.CalendarDatePicker)(target);
                }
                break;
            case 9:
                {
                    this.StartCalendarDatePicker = (global::Windows.UI.Xaml.Controls.CalendarDatePicker)(target);
                    #line 29 "..\..\..\Pages\AddTermin.xaml"
                    ((global::Windows.UI.Xaml.Controls.CalendarDatePicker)this.StartCalendarDatePicker).DateChanged += this.StartCalendarDatePicker_DateChanged;
                    #line default
                }
                break;
            case 10:
                {
                    this.TpVon = (global::Windows.UI.Xaml.Controls.TimePicker)(target);
                    #line 30 "..\..\..\Pages\AddTermin.xaml"
                    ((global::Windows.UI.Xaml.Controls.TimePicker)this.TpVon).TimeChanged += this.TpVon_TimeChanged;
                    #line default
                }
                break;
            case 11:
                {
                    this.TpBis = (global::Windows.UI.Xaml.Controls.TimePicker)(target);
                }
                break;
            default:
                break;
            }
            this._contentLoaded = true;
        }

        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 14.0.0.0")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public global::Windows.UI.Xaml.Markup.IComponentConnector GetBindingConnector(int connectionId, object target)
        {
            global::Windows.UI.Xaml.Markup.IComponentConnector returnValue = null;
            return returnValue;
        }
    }
}
