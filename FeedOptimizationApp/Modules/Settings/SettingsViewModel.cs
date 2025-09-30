using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using CommunityToolkit.Maui.Extensions;
using DataLibrary.Models.Enums;
using DataLibrary.Services;
using FeedOptimizationApp.Helpers;
using FeedOptimizationApp.Localization;
using FeedOptimizationApp.Services;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows.Input;

namespace FeedOptimizationApp.Modules.Settings
{
    public class SettingsViewModel : BaseViewModel
    {
        private readonly BaseService _baseService;
        private readonly DatabaseInitializer _databaseInitializer;
        private readonly LanguageToCodeConverter _languageConverter;

        private LanguageEntity? _initialSelectedLanguage;
        private CountryEntity? _initialSelectedCountry;
        private SpeciesEntity? _initialSelectedSpecies;

        private bool _isSaveButtonClicked;
        private bool _selectionsChanged;

        // Backing fields
        private ObservableCollection<TranslatedItem<LanguageEntity>> _languages = new();
        private ObservableCollection<TranslatedItem<CountryEntity>> _countries = new();
        private ObservableCollection<TranslatedItem<SpeciesEntity>> _speciesList = new();

        public ObservableCollection<TranslatedItem<LanguageEntity>> Languages
        {
            get => _languages;
            private set { _languages = value; OnPropertyChanged(); }
        }

        public ObservableCollection<TranslatedItem<CountryEntity>> Countries
        {
            get => _countries;
            private set { _countries = value; OnPropertyChanged(); }
        }

        public ObservableCollection<TranslatedItem<SpeciesEntity>> SpeciesList
        {
            get => _speciesList;
            private set { _speciesList = value; OnPropertyChanged(); }
        }

        private TranslatedItem<LanguageEntity>? _selectedLanguage;
        public TranslatedItem<LanguageEntity>? SelectedLanguage
        {
            get => _selectedLanguage;
            set
            {
                if (_selectedLanguage != value)
                {
                    _selectedLanguage = value;
                    
                    if (value != null)
                    {
                        SharedData.SelectedLanguage = value.Value;
                        _selectionsChanged = true;
                        
                        var code = _languageConverter.Convert(value.Value);
                        if (!TranslationProvider.IsUpdatingLanguage)
                        {
                            TranslationProvider.SetLanguage(code);
                        }

                        foreach (var item in Languages) item.Refresh();
                        Languages = new ObservableCollection<TranslatedItem<LanguageEntity>>(Languages);
                        OnPropertyChanged(nameof(Languages));
                        foreach (var item in Countries) item.Refresh();
                        Countries = new ObservableCollection<TranslatedItem<CountryEntity>>(Countries);
                        OnPropertyChanged(nameof(Countries));
                        foreach (var item in SpeciesList) item.Refresh();
                        SpeciesList = new ObservableCollection<TranslatedItem<SpeciesEntity>>(SpeciesList);
                        OnPropertyChanged(nameof(SpeciesList));

                        SelectedLanguage = Languages.FirstOrDefault(x => x.Value.Id == SharedData.SelectedLanguage?.Id);
                        SelectedCountry = Countries.FirstOrDefault(x => x.Value.Id == SharedData.SelectedCountry?.Id);
                        SelectedSpecies = SpeciesList.FirstOrDefault(x => x.Value.Id == SharedData.SelectedSpecies?.Id);

                        OnPropertyChanged(nameof(SelectedLanguage));
                        OnPropertyChanged(nameof(SelectedCountry));
                        OnPropertyChanged(nameof(SelectedSpecies));
                    }
                }
            }
        }

        private TranslatedItem<CountryEntity>? _selectedCountry;
        public TranslatedItem<CountryEntity>? SelectedCountry
        {
            get => _selectedCountry;
            set
            {
                if (_selectedCountry != value)
                {
                    _selectedCountry = value;
                    _selectionsChanged = true;
                    OnPropertyChanged();

                    if (value != null)
                        SharedData.SelectedCountry = value.Value;
                }
            }
        }

        private TranslatedItem<SpeciesEntity>? _selectedSpecies;
        public TranslatedItem<SpeciesEntity>? SelectedSpecies
        {
            get => _selectedSpecies;
            set
            {
                if (_selectedSpecies != value)
                {
                    _selectedSpecies = value;
                    OnPropertyChanged();

                    if (value != null)
                        SharedData.SelectedSpecies = value.Value;
                }
            }
        }

        public ICommand CancelCommand { get; }
        public ICommand SaveCommand { get; }

        public SettingsViewModel(
            BaseService baseService,
            SharedData sharedData,
            DatabaseInitializer databaseInitializer,
            TranslationProvider translationProvider,
            LanguageToCodeConverter languageToCodeConverter)
            : base(sharedData, translationProvider)
        {
            _baseService = baseService ?? throw new ArgumentNullException(nameof(baseService));
            _databaseInitializer = databaseInitializer ?? throw new ArgumentNullException(nameof(databaseInitializer));
            _languageConverter = languageToCodeConverter ?? throw new ArgumentNullException(nameof(languageToCodeConverter));

            _initialSelectedLanguage = sharedData.SelectedLanguage;
            _initialSelectedCountry = sharedData.SelectedCountry;
            _initialSelectedSpecies = sharedData.SelectedSpecies;

            CancelCommand = new Command(OnCancelButtonClicked);
            SaveCommand = new Command(async () => await OnSaveButtonClicked());                       

            _ = LoadEnumValuesAsync();
        }

        private void OnCancelButtonClicked()
        {
            SelectedLanguage = Languages.FirstOrDefault(x => x.Value.Id == _initialSelectedLanguage?.Id);
            SelectedCountry = Countries.FirstOrDefault(x => x.Value.Id == _initialSelectedCountry?.Id);
            SelectedSpecies = SpeciesList.FirstOrDefault(x => x.Value.Id == _initialSelectedSpecies?.Id);

            if (_initialSelectedLanguage != null)
                TranslationProvider.SetLanguage(_languageConverter.Convert(_initialSelectedLanguage));
        }

        public void OnDisappearing()
        {
            if (!_isSaveButtonClicked)
            {
                SelectedLanguage = Languages.FirstOrDefault(x => x.Value.Id == _initialSelectedLanguage?.Id);
                SelectedCountry = Countries.FirstOrDefault(x => x.Value.Id == _initialSelectedCountry?.Id);
                SelectedSpecies = SpeciesList.FirstOrDefault(x => x.Value.Id == _initialSelectedSpecies?.Id);

                if (_initialSelectedLanguage != null)
                    TranslationProvider.SetLanguage(_languageConverter.Convert(_initialSelectedLanguage));
            }
        }

        #region Load Enums

        private async Task LoadEnumValuesAsync()
        {
            try
            {
                var languageTask = _baseService.EnumEntitiesService.GetLanguagesAsync();
                var countryTask = _baseService.EnumEntitiesService.GetCountriesAsync();
                var speciesTask = _baseService.EnumEntitiesService.GetSpeciesAsync();

                await Task.WhenAll(languageTask, countryTask, speciesTask);

                if (languageTask.Result.Succeeded)
                    Languages = new ObservableCollection<TranslatedItem<LanguageEntity>>(
                        languageTask.Result.Data.Select(l => new TranslatedItem<LanguageEntity>(l, () => TranslationProvider[$"Language_{l.Name}"])));

                if (countryTask.Result.Succeeded)
                    Countries = new ObservableCollection<TranslatedItem<CountryEntity>>(
                        countryTask.Result.Data.Select(c => new TranslatedItem<CountryEntity>(c, () => TranslationProvider[$"Country_{c.Name}"])));

                if (speciesTask.Result.Succeeded)
                    SpeciesList = new ObservableCollection<TranslatedItem<SpeciesEntity>>(
                        speciesTask.Result.Data.Select(s => new TranslatedItem<SpeciesEntity>(s, () => TranslationProvider[$"Species_{s.Name}"])));

                SelectedLanguage = Languages.FirstOrDefault(x => x.Value.Id == SharedData.SelectedLanguage?.Id);
                SelectedCountry = Countries.FirstOrDefault(x => x.Value.Id == SharedData.SelectedCountry?.Id);
                SelectedSpecies = SpeciesList.FirstOrDefault(x => x.Value.Id == SharedData.SelectedSpecies?.Id);

                
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error loading enums: {ex.Message}");
                await Application.Current.MainPage.DisplayAlert(
                    TranslationProvider["SettingsPage_ErrorTitle"],
                    TranslationProvider["SettingsPage_LoadErrorMessage"],
                    TranslationProvider["SettingsPage_OKButton"]);
            }
        }

        #endregion

        private async Task OnSaveButtonClicked()
        {
            _isSaveButtonClicked = true;

            if (SelectedLanguage?.Value == null || SelectedCountry?.Value == null || SelectedSpecies?.Value == null)
            {
                await Application.Current.MainPage.DisplayAlert(
                    TranslationProvider["SettingsPage_ErrorTitle"],
                    TranslationProvider["SettingsPage_ErrorMessage"],
                    TranslationProvider["SettingsPage_OKButton"]);
                return;
            }

            var popup = new CustomAlertPopup(
                TranslationProvider["SettingsPage_SavePopupTitle"],
                TranslationProvider["SettingsPage_SavePopupMessage"],
                TranslationProvider["SettingsPage_ConfirmButton"],
                TranslationProvider["SettingsPage_CancelButton"],
                async () => await SaveSettingsAsync());

            await Application.Current.MainPage.ShowPopupAsync(popup);
        }

        private async Task SaveSettingsAsync()
        {
            try
            {
                var user = (await _baseService.UserService.GetAllAsync()).Data.FirstOrDefault();
                if (user == null) return;

                user.LanguageId = SelectedLanguage!.Value.Id;
                user.CountryId = SelectedCountry!.Value.Id;
                user.SpeciesId = SelectedSpecies!.Value.Id;

                await _baseService.UserService.UpdateAsync(user);

                // Update initial values
                _initialSelectedLanguage = SelectedLanguage.Value;
                _initialSelectedCountry = SelectedCountry.Value;
                _initialSelectedSpecies = SelectedSpecies.Value;

                if (_selectionsChanged)
                {
                    var toast = Toast.Make(TranslationProvider["SettingsPage_SavingChangesToast"]);
                    await toast.Show();

                    _baseService.ResetPickerService.ResetPicker();
                    SharedData.RequestClearAnimalInfo();

                    await _databaseInitializer.ClearAllCalculationsAsync();
                    await _databaseInitializer.ClearAndRepopulateFeedsAsync(SelectedCountry.Value.Id, SelectedLanguage.Value.Id);

                    await toast.Dismiss();
                }

                await Toast.Make(TranslationProvider["SettingsPage_SuccessToast"], ToastDuration.Long).Show();
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Exception: {ex.Message}");
            }
        }
    }
}
