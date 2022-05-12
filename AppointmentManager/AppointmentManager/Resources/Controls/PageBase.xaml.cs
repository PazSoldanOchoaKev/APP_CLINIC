using System;
using System.Collections.Generic;
using System.ComponentModel;
using Xamarin.Forms;

namespace AppointmentManager.Resources.Controls
{
    [DesignTimeVisible(true)]
    [ContentProperty("ContentBody")]
    public partial class PageBase : ContentPage
    {
        public PageBase()
        {
            InitializeComponent();
        }

        public static readonly BindableProperty HeaderTitleProperty =
            BindableProperty.Create(nameof(HeaderTitle), typeof(string), typeof(PageBase), default(string));

        public string HeaderTitle
        {
            get => (string)GetValue(HeaderTitleProperty);
            set => SetValue(HeaderTitleProperty, value);
        }

        public static readonly BindableProperty ContentBodyProperty =
             BindableProperty.Create(nameof(ContentBody), typeof(View), typeof(PageBase));

        public View ContentBody
        {
            get => (View)GetValue(ContentBodyProperty);
            set => SetValue(ContentBodyProperty, value);
        }
    }
}
