using System.Collections.Generic;
using System.Windows.Input;
using System.Windows;

public class AddAndEditUserVM : MainViewModel
{
    // инициализация модели
    private AddAndEditUserModel _model = new AddAndEditUserModel();
    // ссылка на класс навигации
    private Navigation _navigation;


    // лист типов партнеров
    private List<string> _types;
    public List<string> Types
    {
        get { return _types; }
        set { _types = value; OnPropertyChanged(); }
    }

    // текущая страница(создание или редактирование)
    private string _currentPage;
    public string currentPage
    {
        get { return _currentPage; }
        set { _currentPage = value; OnPropertyChanged(); }
    }

    // если человек перешел чтобы изменить партнера
    private int editPartnerID;

    // поля которые редактируются
    private string _newName;
    public string NewName
    {
        get => _newName;
        set
        {
            if (_newName != value)
            {
                _newName = value;
                OnPropertyChanged();
            }
        }
    }

    private string _newType;
    public string NewType
    {
        get => _newType;
        set
        {
            if (_newType != value)
            {
                _newType = value;
                OnPropertyChanged();
            }
        }
    }

    private string _newFio;
    public string NewFio
    {
        get => _newFio;
        set
        {
            if (_newFio != value)
            {
                _newFio = value;
                OnPropertyChanged();
            }
        }
    }

    private string _newMail;
    public string NewMail
    {
        get => _newMail;
        set
        {
            if (_newMail != value)
            {
                _newMail = value;
                OnPropertyChanged();
            }
        }
    }


    private string _newPhone;
    public string NewPhone
    {
        get => _newPhone;
        set
        {
            if (_newPhone != value)
            {
                _newPhone = value;
                OnPropertyChanged();
            }
        }
    }

    private string _newAddress;
    public string NewAddress
    {
        get => _newAddress;
        set
        {
            if (_newAddress != value)
            {
                _newAddress = value;
                OnPropertyChanged();
            }
        }
    }


    private string _newINN;
    public string NewINN
    {
        get => _newINN;
        set
        {
            if (_newINN != value)
            {
                _newINN = value;
                OnPropertyChanged();
            }
        }
    }


    private float _newRating;
    public float NewRating
    {
        get => _newRating;
        set
        {
            if (_newRating != value)
            {
                _newRating = value;
                OnPropertyChanged();
            }
        }
    }


    // команды для кнопок
    public ICommand goBackCommand { get; set; }
    public ICommand saveDataCommand { get; set; }

    public AddAndEditUserVM(Navigation navigation, int partnerID = -1)
    {
        // устанавливаем изначальный заголовок как создание
        _currentPage = "Добавление партнера";
        // получаем айди партнера
        editPartnerID = partnerID;
        // по умолчанию айди равно -1 дабы отличать происходит сейчас создание или редактирование
        // при редактировании мы получим айди строго больше или равно 0
        // если редактируем, то устанавливаем данные партнера
        if (editPartnerID != -1)
        {
            _currentPage = "Изменение партнера";
            Partners partner = _model.getEditPartner(editPartnerID);
            NewName = partner.PartnerName;
            NewType = partner.PartnerType;
            NewFio = partner.Director;
            NewMail = partner.DirectorMail;
            NewPhone = partner.DirectorPhone;
            NewAddress = partner.DirectorAddress;
            NewINN = partner.INN.ToString("0");
            NewRating = partner.Rating;
        }
        // привязываем данные
        _navigation = navigation;
        Types = _model.types;

        goBackCommand = new RelayCommand(goBack);
        saveDataCommand = new RelayCommand(saveData);
    }

    // метод нажатия назад
    private void goBack(object obj)
    {
        _navigation.CurrentView = new PartnersVM(_navigation);
    }

    // метод нажатия подтвердить
    private void saveData(object obj)
    {
        // проверка данных
        if (_model.checkName(NewName) == false)
        {
            MessageBox.Show("Наименование:\nНе должно быть пустым.\nНе должно превышать длину в 255 символов.", "Изменение наименования", MessageBoxButton.OK, MessageBoxImage.Error);
            NewName = "";
            return;
        }

        if (_model.checkType(NewType) == false)
        {
            MessageBox.Show("Тип должен быть выбран.", "Изменение типа", MessageBoxButton.OK, MessageBoxImage.Error);
            return;
        }
        if (_model.checkFio(NewFio) == false)
        {
            MessageBox.Show("ФИО:\nНе должно быть пустым.\nНе должно превышать длину в 255 символов.", "Изменение ФИО", MessageBoxButton.OK, MessageBoxImage.Error);
            NewFio = "";
            return;
        }
        if (_model.checkMail(NewMail) == false)
        {
            MessageBox.Show("Почта:\nНе должна быть пустой.\nНе должна превышать длину в 255 символов.", "Изменение почты", MessageBoxButton.OK, MessageBoxImage.Error);
            NewMail = "";
            return;
        }
        if (_model.checkPhone(NewPhone) == false)
        {
            MessageBox.Show("Телефон:\nНе должен быть пустым.\nНе должно превышать длину в 15 символов(8(999)888-77-66).", "Изменение телефона", MessageBoxButton.OK, MessageBoxImage.Error);
            NewPhone = "";
            return;
        }
        if (_model.checkAddress(NewAddress) == false)
        {
            MessageBox.Show("Адрес:\nНе должен быть пустым.\nНе должен превышать длину в 255 символов.", "Изменение адреса", MessageBoxButton.OK, MessageBoxImage.Error);
            NewAddress = "";
            return;
        }
        if (_model.checkINN(NewINN) == false)
        {
            MessageBox.Show("ИНН:\nНе должен быть пустым.\nНе должен превышать длину в 10 символов.", "Изменение ИНН", MessageBoxButton.OK, MessageBoxImage.Error);
            NewINN = "";
            return;
        }

        if (_model.checkRating(NewRating) == false)
        {
            MessageBox.Show("Рейтинг должен быть неотрицательным число от 0 до 10!", "Изменение рейтинга", MessageBoxButton.OK, MessageBoxImage.Error);
            NewRating = 0;
            return;
        }

        // проверка айди для создания или редактирования партнера
        if (editPartnerID == -1)
        {
            _model.AddNewUser(NewName, NewType, NewFio, NewMail, NewPhone, NewAddress, NewINN, NewRating);
        }
        else
        {
            _model.EditUser(editPartnerID, NewName, NewType, NewFio, NewMail, NewPhone, NewAddress, NewINN, NewRating);
        }

        // переход назад
        _navigation.CurrentView = new PartnersVM(_navigation);
    }
}
