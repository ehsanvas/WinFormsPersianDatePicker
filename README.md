# WinFormsPersianDatePicker
It is a Persian Date Picker with beautiful UI for .NET Windows Forms projects.<br/>
![clan](https://user-images.githubusercontent.com/5195633/160150125-7c1ce3fc-a0f7-4bfa-a021-a7c88f9c1045.jpg)<br/><br/><br/>
You can use it like the sample project very easily.
It has an event handler that fires on date changed:<br/>
```C#
_datePicker.SelectedDateChange += _datePicker_SelectedDateChange;
```
you can also get the selected datetime with SelectedDate propery:<br/>
```C#
DateLabel.Text = ((WinFormsPersianDatePicker)sender).SelectedDate.ToString();
```
