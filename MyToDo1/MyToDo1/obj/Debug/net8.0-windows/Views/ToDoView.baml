   M S B A M L   `   `   `  ����B  >MyToDo1, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null-clr-namespace:MyToDo1.ViewsMyToDo1.Views  5          MyToDo1.Views.ToDoView   5      W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      5      R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      _ [MaterialDesignThemes.Wpf, Version=5.1.0.0, Culture=neutral, PublicKeyToken=df2a72020bd7962a:md1http://materialdesigninxaml.net/winfx/xaml/themes  5      :d2http://schemas.microsoft.com/expression/blend/2008  5      'localclr-namespace:MyToDo1.Views  5      .��5
      )  #MaterialDesignThemes.Wpf.DialogHost  .��5   
   )  #MaterialDesignThemes.Wpf.DrawerHost      IsRightDrawerOpen  6   �� *IsRightDrawerOpen+   RightDrawerContent 5      ]� 5      $	��300��6   $��False��6(   .+�5      �� $��添加待办��6    $��20(�6,   $
��Bold"�6:   $	��Top=�6L   $��15q�6a   �� 5       �� Orientation$ 
Horizontal=�6!   $	��Top=�6:   $��15q�6O   .+�5      �� $��	状态：��6$   $��0,5,15,0q�6/   $
��Left=�6A   �� 5      $
��Left=�6#   $��10��6>   .@�5      �� .��6+   待办6/   �� 5      .��6+   	已完成60   5      5      �� 5      )  #MaterialDesignThemes.Wpf.HintAssist   Hint$ 请输入待办概要��6   $	��Top=�6;   $��15q�6P   �� 5      $ 请输入待办内容��6   $	��Top=�6;   $��15q�6P   �� 5      $��添加到待办��6   $	��Top=�6-   $��15q�6B   5      .��5!      �  � RowDefinitions 5"      �� 5#      $
��Auto �6$   �� 5$      .+�5'      �� $ 
Horizontal=�6   .+�5(      �� $��待办事项��6    $��26(�6,   $��20,20q�6:   $��Bottom=�6I   $
��Bold"�6d   �� 5)      $	��250��5*      $��Bottom=�5+      $ 查找待办事项��5,      .  (MaterialDesignThemes.Wpf.TextFieldAssist   HasClearButton$
 True��5-      $��10,20q�5.      �� 5/      $��	筛选：��50      $��
10,0,20,20q�51      $��Bottom=�52      $��20(�53      �� 54       �� SelectedIndex$ 0��6   $��Bottom=�61   $��	0,0,10,20q�6L   .@�55      �� .��6'   全部6+   �� 56      .��6'   待办6+   �� 57      .��6'   	已完成6,   58      �� 5:      $
��Left=�6   $��+ 添加代办��68   $��Bottom=�5;   <   $��	10,0,0,20q�5<      ��5;      �� *
AddCommand+5<      5=      �� 5?      ���#dddddd$��0,0,0,1q�6/   �� 5A      $
��Auto=�6   $��1��6B   .��5B      �� $��1��6#   ��60   �� *
ToDoDtos+��5C      �� 5D      .R�5E   "   � 5F      ��5I      �� 5J      .R�5K   "   � $	��220��5L      $	��180��5M      $	��250��5N      $��15q�5O       5Q   &   �� 5R   *   $
��auto �68   �� 5S   *   .+�5V   &   ��  �� CornerRadius$ 10��6-   $��2��6?   ���#b7a57a$��Stretch=�6e   �� 5X   &   $
��Left=�6G   $��
15,15,0, 0q�6b   $
��Bold"�6v   ��60   �� *Title+6v   �� 5Y   &   $��1��6I   $
��Left=�6V   $��15,15q�6q   ��60   �� *	Content+6q   �� 5[   &   $��2��6-   $
}�True��6>   .+�5\   *   �� 
 �� Top$ 10��5]   -   	 �� Right$		 -50��6=   $	��120��5^   -   $	��120��69   ���#FFFFFF
 J� Opacity$	
 0.1Q�5`   -   $	 100��5a   -   �� 5b   *   $ 80��5c   -   $		 -30��6=   $	��120��5d   -   $	��120��69   ���#FFFFFF$	
 0.1Q�5f   -   $	 100��5g   -   5h   '   5j   #   5k      5n      5o      5r      5s      5t      5v      