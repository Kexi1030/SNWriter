   M S B A M L   `   `   `  ����I  EDatsTestSystem, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nulle7clr-namespace:DatsTestSystem.HardwareSerialNumberWirter)DatsTestSystem.HardwareSerialNumberWirter  5   	   T    NDatsTestSystem.HardwareSerialNumberWirter.HardwareSerialNumberWriterMainWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   Ilocal7clr-namespace:DatsTestSystem.HardwareSerialNumberWirter     5   	   -   5          � Title$*  $HardwareSerialNumberWriterMainWindow��5	   	   $	��500��66   $	��800��6C    � 
ResizeMode$ NoResize=�6O   .��5
      �  � RowDefinitions 5   
   �� 5      $
��1.5* �6   �� 5      $��2* �6   �� 5      $
��2.5* �6   �� 5      $��8* �6   �� 5      $��2* �6   �� 5      $��1* �6    � ColumnDefinitions 5   
   �� 5      $��4* �6   �� 5      $��5* �6   �� 5      $��2* �6   .+�5   
   c� $��0��6   $��3��6   .@�5      �� -   5         ��Name$ PortControllerButton��5      $��串口管理��63   $��14(�6B   6P   5      �� 5   
    �� Orientation$ 
Horizontal=�6   $��4��6.   $��0��6;   $��Center=�6K   .+�5      �� -   5        $ InitialButton��5      $��新建��6,   $��24(�69   $��70��6G   $��5q�6R   6]   �� 5      -   5        $! InitialSNListFromFileButton��5      $��导入��6:   $��24(�6G   $��70��6U   $��5q�6`   6k   �� 5       -   5        $ AddOneSNstringButton��5       $��+��63   $��28(�6?   $��70��6M   $��5q�6X   6c   5!      �� 5#   
   $ 
Horizontal=�6   $��1��6.   $��1��6;   $��2��6L   $��Center=�6`   .+�5$      �� $��当前操作人为:��6   $��24(�6&   $��5q�64   �� 5%      -   5         ��Name$ OperatorNameTextBlock��5%      $��26(�68   $	��240��6i   $��5q�6u   ��6F   ��  �� Path$ operatorname�6u   �� 5&      -   5        $ ModifyUserName��5&      $��修改��6-   $��24(�6:   $��70��6H   $��5q�6S   6^   5'      �� 5)   
   $ Vertical=�6   $��3��6,   $��2��69   $��2��6J   $��Center=�6Z   .+�5*      �� -   5        $ StartButton��5*      $��开始��6*   $��24(�67   $	��200��6E   $��10q�6R   6^   �� 5+      -	   5        $ 	EndButton��5+      $��结束��6(   $��24(�65   $��50��6C   $��10q�6O   6[   5,      �� 5.   
   $��硬件序列号列表��6   $��15(�6$   $��1��62   $��3��6?   $��0��6P   $��5q�6`   .��5/      �� -
   5         ��Name$ SNList��5/      $��5q�6&   61   50      �� 52   
   $��当前序列号��6   $��15(�6"   $��2��60   $��1��6=   $��2��6M   $��5q�6a   .��53      �� -   5        $ CurrentSNTextBlock��53      $��5q�64   54      �� 56   
   $��状态信息��6   $��15(�6!   $��3��6/   $��2��6<   $��1��6M   $��5q�6]   .��57      �� $
��Auto=�6   ��58      �� 59      -   5        $ SNFWStatusTextBlock��59       $��13(�6=   $
��Wrap=�6K   $��5q�6_   5;      5<      �� 5>   
   $��5��6   $��3��6!   5@      5A      5B      