using LUCWQ.MapAPI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace LUCWQ.MapAPIUserControl
{
    public sealed partial class BDSuggestionPlaceControl : UserControl
    {
        //最后选择的地点
        public Result SelectedSuggestion { get; set; } = new Result();

        //建议地点列表
        private ObservableCollection<Result> SuggestPlace = new ObservableCollection<Result>();
        public BDSuggestionPlaceControl()
        {
            this.InitializeComponent();
        }
        private void MySuggestBox_TextChanged(AutoSuggestBox sender, AutoSuggestBoxTextChangedEventArgs args)
        {
            // 因为用户的输入而使得 Text 发生变化
            if (args.Reason == AutoSuggestionBoxTextChangeReason.UserInput || args.Reason == AutoSuggestionBoxTextChangeReason.SuggestionChosen)
            {
                BDMapAPI.GetSuggestPlaceByName(sender.Text, "全国", SuggestPlace);
                MySuggestBox.ItemsSource = SuggestPlace;
            }
            // 通过代码使 Text 发生变化
            //else if (args.Reason == AutoSuggestionBoxTextChangeReason.ProgrammaticChange)
            //{
            //}
            // 因为用户在建议框（即下拉部分）选择了某一项而使得 Text 发生变化
            //else if (args.Reason == AutoSuggestionBoxTextChangeReason.SuggestionChosen)
            //{   
            //}
        }
        private void MySuggestBox_QuerySubmitted(AutoSuggestBox sender, AutoSuggestBoxQuerySubmittedEventArgs args)
        {
            if (args.ChosenSuggestion != null)
            {
                SelectedSuggestion = (Result)args.ChosenSuggestion;
            }
            else
            {
                SelectedSuggestion.name = MySuggestBox.Text;
            }
        }

        private void MySuggestBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if(string.IsNullOrEmpty(SelectedSuggestion.name))
            {
                SelectedSuggestion.name = MySuggestBox.Text;
            }
        }
    }
}
