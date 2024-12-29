namespace CanonUtility.ViewModel;

public partial class AppViewModel : ObservableObject
{
    public AppViewModel()
    {
        //Settings.Default.UpgradeFirst();



        //BaseEngine.ProgressState += OnProgressState;
        //BaseEngine.ProgressValue += OnProgressValue;
        //BaseEngine.ProgressText += OnProgressText;
    }

    /// <summary>
    /// Check if update exists and start updating
    /// </summary>
    /// <param name="pathes"></param>
    /// <remarks>Call on OnActivate</remarks>
    //protected virtual void CheckForUpdate(List<string> pathes)
    //{
    //    Version appVersion = Assembly.GetAssembly(this.GetType()).GetName().Version;
    //    Trace.TraceInformation("Current app version: {0}", appVersion);

    //    string infoPath = pathes.Select(p => Path.Combine(p, "Update.info")).FirstOrDefault(f => File.Exists(f));
    //    Trace.TraceInformation("Update.info path: {0}", infoPath);
    //    if (!string.IsNullOrEmpty(infoPath))
    //    {
    //        try
    //        {
    //            Update update = Update.Load(infoPath);
    //            string msiPath = update.Locations.FirstOrDefault(f => File.Exists(f));
    //            Version updateVersion = Version.Parse(update.Version);
    //            Trace.TraceInformation("Update.info version: {0} msi path: {1}", updateVersion, msiPath);
    //            if (updateVersion > appVersion && !string.IsNullOrEmpty(msiPath))
    //            {
    //                if (MessageBox.Show("A new application version exists.\r\nWould you like to update?", "Info", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
    //                {
    //                    Trace.TraceInformation("Start update");
    //                    Process.Start(msiPath);
    //                    Application.Current.MainWindow.Close();
    //                }
    //            }
    //        }
    //        catch(Exception ex)
    //        {
    //            Trace.TraceError(ex.ToString());
    //        }
    //    }
    //}



    #region Title

    /// <summary>
    /// The main title of the application. Displayed in the main window header and in the taskbar.
    /// </summary>
    public virtual string Title
    {
        get
        {
            Assembly? app = Assembly.GetEntryAssembly();
            return app!.GetCustomAttribute<AssemblyTitleAttribute>()!.Title + " " + app!.GetName().Version!.ToString(2);
        }
    }

    #endregion

    #region Progress

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(ShowProgress))]
    private TaskbarItemProgressState progressState = TaskbarItemProgressState.None;

    public Visibility ShowProgress => ProgressState == TaskbarItemProgressState.None ? Visibility.Collapsed : Visibility.Visible;

    [ObservableProperty]
    private double progressValue = 0.0;

    [ObservableProperty]
    private string statusText = "Ready";


    //private void OnProgressState(ProgressStateEventArgs args)
    //{
    //    this.ProgressState = args.State;
    //}

    //private void OnProgressValue(ProgressValueEventArgs args)
    //{
    //    this.ProgressValue = args.Value;
    //}

    //private void OnProgressText(ProgressTextEventArgs args)
    //{
    //    this.StatusText = args.Text;
    //}

    public bool IsWorking => false;

    #endregion

    #region command methods

    [RelayCommand]
    public virtual void OnStartup()
    {
        if (Application.Current == null)
        {
            // for testing
            OnActivate();
        }
        else
        {
            Application.Current.Dispatcher.BeginInvoke(new Action(() => OnActivate()), DispatcherPriority.ContextIdle, null);
        }
    }

    protected virtual void OnActivate()
    { }

    protected virtual bool OnCanRefresh => true;

    [RelayCommand(CanExecute = nameof(OnCanRefresh))]
    protected virtual void OnRefresh()
    { }

    protected virtual bool OnCanImport => this.ProgressState == TaskbarItemProgressState.None;


    [RelayCommand(CanExecute = nameof(OnCanImport))]
    protected virtual void OnImport()
    { }



    protected virtual bool OnCanExport => this.ProgressState == TaskbarItemProgressState.None;

    [RelayCommand(CanExecute = nameof(OnCanExport))]
    protected virtual void OnExport()
    { }

    protected virtual bool OnCanUndo => false;

    [RelayCommand(CanExecute = nameof(OnCanUndo))]
    protected virtual void OnUndo()
    { }


    protected virtual bool OnCanRedo => false;

    [RelayCommand(CanExecute = nameof(OnCanRedo))]
    protected virtual void OnRedo()
    { }

    protected virtual bool OnCanOptions => true;

    [RelayCommand(CanExecute = nameof(OnCanOptions))]
    protected virtual void OnOptions()
    { }


    [RelayCommand]
    protected virtual void OnAbout()
    { }

    [RelayCommand]
    protected virtual void OnHelp()
    {
        string path = Path.ChangeExtension(Assembly.GetEntryAssembly()!.Location, ".chm");
        if (File.Exists(path))
        {
            System.Diagnostics.Process.Start(path);
        }
        else
        {
            MessageBox.Show(string.Format("Help file \"{0}\" not found!", path), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }

    protected virtual bool OnCanExit => true;


    [RelayCommand(CanExecute = nameof(OnCanExit))]
    protected virtual void OnExit()
    {
        Application.Current.MainWindow.Close();
    }

    [RelayCommand]
    private void OnClosing(CancelEventArgs e)
    {
        if (e != null)
        {
            e.Cancel = !OnClosing();
        }
    }

    protected virtual bool OnClosing() => true;

    #endregion
}