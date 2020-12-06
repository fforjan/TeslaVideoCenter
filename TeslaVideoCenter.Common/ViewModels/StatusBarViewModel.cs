using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using ReactiveUI;
using TeslaVideoCenter.Common.Services;

namespace TeslaVideoCenter.Common.ViewModels
{
    public class StatusBarViewModel : ViewModelBase
    {

        private List<string> infos;
        private object infoTimer;
        private int currentIndex = 0;
        private string info;

        public StatusBarViewModel()
        {
            this.infos = new List<string> {
                "All product and company names are trademarks™ or registered® trademarks of their respective holders.",
                };
            this.info = infos[currentIndex];

            this.GetInfoContent();

            this.HelpMeCommand = ReactiveCommand.Create(() => OpenUrl.WithSystemBrowser("https://github.com/fforjan/TeslaVideoCenter/wiki"));
        }

        public void GetInfoContent(){
            Task.Run(async () =>
            {
                try
                {
                    var content = await OpenUrl.AndReturnContent($"https://raw.githubusercontent.com/wiki/fforjan/TeslaVideoCenter/Announcement-{this.Version}.md");

                    this.infos.AddRange(content.Split("\n", StringSplitOptions.RemoveEmptyEntries).Select(_ => _.Trim()));
                    this.infoTimer = Observable
                        .Interval(TimeSpan.FromSeconds(5))
                        .Select(_ =>
                                {
                                    var current = infos[currentIndex];
                                    currentIndex = (++currentIndex) % infos.Count;
                                    return current;
                                })
                            .Subscribe(
                            onNext: x => Info = x);
                            }
                catch
                { // FIXME  : log
                }
            }
            );
        }

        public ICommand HelpMeCommand { get; }

        public string Version { get => GetType().Assembly.GetName().Version.ToString(2); }

        public string Info { get => info; set => this.RaiseAndSetIfChanged(ref this.info, value); }

    }
}