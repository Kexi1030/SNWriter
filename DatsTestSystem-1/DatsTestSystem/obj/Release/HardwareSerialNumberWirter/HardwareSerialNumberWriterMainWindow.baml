   M S B A M L   `   `   `  ���� I  EDatsTestSystem, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nulle7clr-namespace:DatsTestSystem.HardwareSerialNumberWirter)DatsTestSystem.HardwareSerialNumberWirter  T    NDatsTestSystem.HardwareSerialNumberWirter.HardwareSerialNumberWriterMainWindow   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     Ilocal7clr-namespace:DatsTestSystem.HardwareSerialNumberWirter     -     � Title$*  $HardwareSerialNumberWriterMainWindow��$	��500��$	��800�� � 
ResizeMode$ NoResize=� � 	Resources  �� 
TargetType �� Property �� Value �� 
TargetType �� Triggers �� Binding	 �� Path
 �� Value��%�   '��       �� " ��.���  � �� �� " ��.R��� #��z��._��� #��z��#��z��#��z�� ��  �� $
	 done�
 1.a���  � ��
 � � ���  �� $
	 done�
 -1.a���  � ��
 �  ��.���  � RowDefinitions �� $
��1.5* ��� $��2* ��� $
��2.5* ��� $��8* ��� $��2* ��� $��1* � � ColumnDefinitions �� $��4* ��� $��5* ��� $��2* �.+�c� $��0��$��3��.@��� -    ��Name$ PortControllerButton��$��串口管理��$��14(���  �� Orientation$ 
Horizontal=�$��4��$��0��$��Center=�.+��� -   $ InitialButton��$��新建��$��24(�$��70��$��5q��� -   $! InitialSNListFromFileButton��$��导入��$��24(�$��70��$��5q��� -   $ AddOneSNstringButton��$��+��$��28(�$��70��$��5q��� $ 
Horizontal=�$��1��$��1��$��2��$��Center=�.+��� $��当前操作人为:��$��24(�$��5q��� -    ��Name$ OperatorNameTextBlock��$��26(�$	��240��$��5q����� $	 operatorname��� -   $ ModifyUserName��$��修改��$��24(�$��70��$��5q��� $ Vertical=�$��3��$��2��$��2��$��Center=�.+��� -   $ StartButton��$��开始��$��24(�$	��200��$��10q��� -	   $ 	EndButton��$��结束��$��24(�$��50��$��10q��� $��硬件序列号列表��$��15(�$��1��$��3��$��0��$��5q�.���� -
    ��Name$ SNList��$��5q� �� DisplayMemberPath$ snstring���� $��当前序列号��$��15(�$��2��$��1��$��2��$��5q�.���� -   $ CurrentSNTextBlock��$��5q��� $��状态信息��$��15(�$��3��$��2��$��1��$��5q�.���� $
��Auto=����� -    ��Name$ SNFWStatusTextBlock��$��13(� �� TextWrapping$
 Wrap=�$��5q��� $��5��$��3��