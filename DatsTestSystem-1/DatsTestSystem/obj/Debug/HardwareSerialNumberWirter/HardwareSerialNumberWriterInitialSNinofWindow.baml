   M S B A M L   `   `   `  ����I  EDatsTestSystem, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nulle7clr-namespace:DatsTestSystem.HardwareSerialNumberWirter)DatsTestSystem.HardwareSerialNumberWirter  5   	   ]    WDatsTestSystem.HardwareSerialNumberWirter.HardwareSerialNumberWriterInitialSNinofWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   Ilocal7clr-namespace:DatsTestSystem.HardwareSerialNumberWirter     5   	     � Title$  InitializationInput��5      $	��370��6   $	��370��6+    � 
ResizeMode$ NoResize=�67   .��5	      �  � RowDefinitions 5
   
   �� 5      $��1* �6   �� 5      $��1* �6   �� 5      $��1* �6   �� 5      $��1* �6   �� 5      $��1* �6   �� 5      $��1* �6   �� 5      $��1* �6   .+�5   
   ��  �� Orientation$ 
Horizontal=�6   $��0��6.   $��5q�6;   .+�5      �� $��请选择型号:��6   $	��120��6%   $��5q�61   �� 5      -   5         ��Name$ ModelSelectComboBox��5      $	��190��64   $��5q�6@   5      �� 5   
   $ 
Horizontal=�6   $��1��6.   $��5q�6;   .+�5      �� $��请选择PCBA厂家:��6   $	��120��6)   $��5q�65   �� 5      -   5        $ PCBASelectComboBox��5      $	��190��63   $��5q�6?   5      �� 5   
   $ 
Horizontal=�6   $��2��6.   $��5q�6;   .+�5      �� $��请选择年份和周次:��6   $	��120��6(   $��5q�64   �� 5       -   5        $ YearSelectComboBox��5       $��90��63   $��5q�6>    �� 
IsEditable . 6[   �� 5!      -   5        $ WeekSelectComboBox��5!      $��90��63   $��5q�6>    . 6[   5"      �� 5$   
   $ 
Horizontal=�6   $��3��6.   $��5q�6;   .+�5%      �� $��请输入编号:��6   $	��120��6%   $��5q�61   �� 5&      -   5         ��Name$ SerialNumberTextBox��5&      $	��190��63   $��5q�6?   5'      �� 5)   
   $ 
Horizontal=�6   $��4��6.   $��5q�6;   .+�5*      �� $��请输入硬件版本号:��6   $	��120��6(   $��5q�64   �� 5+      -   5        $ HardWareNumberTextBox��5+      $	��190��65   $��5q�6A   5,      �� 5.   
   $ 
Horizontal=�6   $��5��6.   $��5q�6;   .+�5/      �� $��请输入固件版本号:��6   $	��120��6(   $��5q�64   �� 50      -   5        $ FirmWareNumberTextBox��50      $	��190��65   $��5q�6A   51      �� 53   
   $ 
Horizontal=�6   $��6��6.   $��Right=�6;   $��0,5,22,5q�6W   .+�54      �� -   5         ��Name$ OKButton��54      $��确定��6'   $��90��64   $��5q�6?   6J   �� 55      -	   5        $ CancelButton��55      $��取消��6+   $��90��68   $��5q�6C   6N   56      57      58      