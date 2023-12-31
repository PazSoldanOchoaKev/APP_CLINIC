﻿using System;
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

        public static readonly BindableProperty FotterProperty =
             BindableProperty.Create(nameof(Fotter), typeof(View), typeof(PageBase));

        public View Fotter
        {
            get => (View)GetValue(FotterProperty);
            set => SetValue(FotterProperty, value);
        }

        public static readonly BindableProperty ScrollModeProperty =
             BindableProperty.Create(nameof(ScrollMode), typeof(ScrollOrientation), typeof(PageBase), defaultValue: ScrollOrientation.Vertical);

        public ScrollOrientation ScrollMode
        {
            get => (ScrollOrientation)GetValue(ScrollModeProperty);
            set => SetValue(ScrollModeProperty, value);
        }
    }
}
