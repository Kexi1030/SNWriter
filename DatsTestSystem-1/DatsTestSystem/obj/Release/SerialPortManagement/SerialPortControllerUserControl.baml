   M S B A M L   `   `   `  ���� I  EDatsTestSystem, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullY1clr-namespace:DatsTestSystem.SerialPortManagement#DatsTestSystem.SerialPortManagement  >    8DatsTestSystem.SerialPortManagement.SerialPortController   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     Clocal1clr-namespace:DatsTestSystem.SerialPortManagement     .��� .+��� $��端口设置��$��20(�.���   � RowDefinitions  �� $��1* ��� $��1* ��� $��1* ��� $��1* ��� $��1* �.+���  �� Orientation$ 
Horizontal=�$��5q�$��0��.+��� -   $��PortNamesComboBox��$��
5,10,15,10q�$��20(�$	��220���� $��端口��$��5q�$��20(��� $ 
Horizontal=�$��5q�$��1��.+��� -   $��BaudRateComboBox��$��
5,10,15,10q�$��20(�$	��220���� $��	波特率��$��5q�$��20(��� $ 
Horizontal=�$��5q�$��2��.+��� -   $��DataBitsComboBox��$��
5,10,15,10q�$��20(�$	��220���� $��	数据位��$��5q�$��20(��� $ 
Horizontal=�$��5q�$��3��.+��� -   $��StopBitsComboBox��$��
5,10,15,10q�$��20(�$	��220���� $��	停止位��$��5q�$��20(��� $ 
Horizontal=�$��5q�$��4��.+��� -   $��ParityComboBox��$��
5,10,15,10q�$��24(�$	��220���� $��	校验位��$��5q�$��20(�