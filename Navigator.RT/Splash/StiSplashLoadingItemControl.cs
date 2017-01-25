#region Copyright (C) 2003-2013 Stimulsoft
/*
{*******************************************************************}
{																	}
{	Stimulsoft Reports.RT											}
{	                         										}
{																	}
{	Copyright (C) 2003-2013 Stimulsoft     							}
{	ALL RIGHTS RESERVED												}
{																	}
{	The entire contents of this file is protected by U.S. and		}
{	International Copyright Laws. Unauthorized reproduction,		}
{	reverse-engineering, and distribution of all or any portion of	}
{	the code contained in this file is strictly prohibited and may	}
{	result in severe civil and criminal penalties and will be		}
{	prosecuted to the maximum extent possible under the law.		}
{																	}
{	RESTRICTIONS													}
{																	}
{	THIS SOURCE CODE AND ALL RESULTING INTERMEDIATE FILES			}
{	ARE CONFIDENTIAL AND PROPRIETARY								}
{	TRADE SECRETS OF Stimulsoft										}
{																	}
{	CONSULT THE END USER LICENSE AGREEMENT FOR INFORMATION ON		}
{	ADDITIONAL RESTRICTIONS.										}
{																	}
{*******************************************************************}
*/
#endregion Copyright (C) 2003-2013 Stimulsoft

using System;
using Windows.UI;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Animation;

namespace Navigator.RT.Splash
{
    internal sealed class StiSplashLoadingItemControl : UserControl
    {
        #region Fields
        private StackPanel _panel;
        private TextBlock _textBlockLabel;
        private readonly int state;
        #endregion

        #region Methods
        private void InitializeComponent()
        {
            _textBlockLabel = new TextBlock
                                  {
                                      TextWrapping = Windows.UI.Xaml.TextWrapping.Wrap,
                                      MinWidth = 250,
                                      FontSize = 15,
                                      Opacity = 0
                                  };

            _panel = new StackPanel
                         {
                             Orientation = Orientation.Horizontal
                         };
            _panel.Children.Add(_textBlockLabel);

            this.Content = _panel;
            this.Loaded += ThisLoaded;
        }
        #endregion

        #region Handlers
        private void ThisLoaded(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            var storyboard = new Storyboard();

            var anim = new DoubleAnimation
                           {
                               Duration = new Windows.UI.Xaml.Duration(TimeSpan.FromMilliseconds(1500)),
                               To = 1
                           };
            Storyboard.SetTarget(anim, _textBlockLabel);
            Storyboard.SetTargetProperty(anim, "TextBlock.Opacity");
            storyboard.Children.Add(anim);

            if (state == 2)
            {
                var colorAnim = new ColorAnimation
                                    {
                                        BeginTime = TimeSpan.FromMilliseconds(1500),
                                        Duration = new Windows.UI.Xaml.Duration(TimeSpan.FromMilliseconds(1000)),
                                        From = Color.FromArgb(255, 255, 0, 0),
                                        To = Color.FromArgb(255, 255, 125, 35),
                                        AutoReverse = true,
                                        RepeatBehavior = new RepeatBehavior(5)
                                    };
                Storyboard.SetTarget(colorAnim, _textBlockLabel);
                Storyboard.SetTargetProperty(colorAnim, "(TextBlock.Foreground).Color");
                storyboard.Children.Add(colorAnim);
            }

            storyboard.Begin();
        }
        #endregion

        public StiSplashLoadingItemControl(string labelText, int state)
        {
            // state:
            // 0 - Header, Footer
            // 1 - Init
            // 2 - Error
            this.state = state;
            this.InitializeComponent();

            this._textBlockLabel.Text = labelText;
            this.Margin = new Windows.UI.Xaml.Thickness(0, 2, 0, 2);

            switch (state)
            {
                case 0:
                    _textBlockLabel.Foreground = Brushes.White;
                    break;

                case 1:
                    _textBlockLabel.Foreground = new SolidColorBrush(Color.FromArgb(255, 255, 125, 35));
                    break;

                case 2:
                    _textBlockLabel.Foreground = Brushes.Red;
                    break;
            }
        }
    }
}