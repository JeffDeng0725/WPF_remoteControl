   M S B A M L   `   `   `  ����B  >MyToDo1, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null-clr-namespace:MyToDo1.ViewsMyToDo1.Views  5      7 clr-namespace:MyToDo1.ViewModelsMyToDo1.ViewModels  5   W       MyToDo1.Views.IndexView   5      W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      5      R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      -localclr-namespace:MyToDo1.Views     5      _ [MaterialDesignThemes.Wpf, Version=5.1.0.0, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5      1
viewmodels clr-namespace:MyToDo1.ViewModels  6W   .��5
      �   � RowDefinitions  5   
   �� 5      $
��auto �6   �� 5      $
��auto �6   �� 5      .+�5   
   ]� $��False��6   .+�5      �� $��首页��6   $��26(�6"   $��20,20q�60   $��Bottom=�6?   $��	UltraBold"�6Z   �� 5      -   5        $��DateTextBlock��5      $��20(�6-   $
��0,20q�6;   $��Bottom=�6I   $��Right=�6d   5      �� 5   
   ���#dddddd$��0,0,0,1q�6'   �� 5   
   $��1��6   $��0,15,0,0q�6E   ��6$   �� *
TaskBars+��5      �� 5      .R�5      A�  A� Columns$ 4��6"   5      ��5      �� 5       .R�5!      �� $
��15,0q�6:    �� CornerRadius$ 10��6H   ��6   �� *Color+��5"      �� 5#       �� 
TargetType" ��6$    �� Triggers 5$   "   P� 5%   &    P� Property �@J�IsMouseOver P� Value$
 True��6E   . �5&   *   ��  �� Property �@J�Effect �� Value 5'   .   3  -System.Windows.Media.Effects.DropShadowEffect  5(   2   	   Color$	 #DDDDDD��5)   5   
   ShadowDepth$
 1Q�5*   5      
BlurRadius$ 10Q�5+   5   5-   +   5/   '   51      ._�53      � .+�54      �� $��20,10q�6)   $
��Left=�68   .+�55   "   '  !MaterialDesignThemes.Wpf.PackIcon  $��30��57   %   $��30��58   %      Kind 56   %   �� *Icon+58   %   �� 5:   "   $
��0,15q�5<   %   $��15(�5=   %   ��5;   %   �� *Title+5=   %   �� 5?   "   $
��Bold"�5A   %   $��40(�5B   %   ��5@   %   �� *	Content+5B   %   5D      �� 5F      $
}�True��6%   .+�5G   "   �� 
 �� Top$ 10��6)    �� Right$	 -50��69   $	��120��5H   )   $	��120��65   ���#FFFFFF J� Opacity$	 0.1Q�5J   )   $	 100��5K   )   �� 5L   "   $ 80��6)   $	 -30��69   $	��120��5M   )   $	��120��65   ���#FFFFFF$	 0.1Q�5O   )   $	 100��5P   )   5Q      5R      5S      5T      5V      � 5X   
   $��2��6   $
��0,15q�6    � ColumnDefinitions 5Y      �� 5Z      �� 5[      .+�5^      �� $��0��5_      $
��15,0q�5`      $ 10��5a      $	 0.1Q�5b      ���#BEBEBE5c      �� 5e      $��1��5f      $
��15,0q�5g      $ 10��5h      $	 0.1Q�5i      ���#BEBEBE5j      ]� 5l      $
��15,0q�6   .+�5m      ]� $��15,15q�5n      $	��Top=�5o      $��False��5p      .+�5q      �� $��待办事项��6    $��20(�5r       $
��Bold"�5s       �� 5t      $��30��6   $��30��5u      $��Right=�5v      $	��Top=�5w       /  +MaterialDesignFloatingActionSecondaryButton#��[  5x      .��5z        $	 Add=�62   5{      5~      �� 5�      $��Stretch=�5�      ��5�      �� *
ToDoDtos+��5�      �� 5�      .R�5�      ]� $��80��6(   $��False��67   .+�5�   "   d� $��Right=�6/   �� 5�   "   .+�5�   &   �� $��16(�5�   1   $
��Bold"�5�   1   ��5�   0   �� *Title+5�   1   �� 5�   &   $
��0,10q�5�   1   $	 0.5Q�5�   1   ��5�   0   �� *	Content+5�   1   5�   #   5�      5�      5�      5�      ]� 5�      $
��15,0q�6   $��1��6&   .+�5�      ]� $��15,15q�5�      $	��Top=�5�      $��False��5�      .+�5�      �� $��	备忘录��5�      $��20(�5�      $
��Bold"�5�      �� 5�      $��30��5�      $��30��5�      $��Right=�5�      $	��Top=�5�      #��[  5�      .��5�        $	 Add=�62   5�      5�      �� 5�      ��6   �� *
MemoDtos+��5�      �� 5�      .R�5�      �� $��80��6)   .+�5�   "   �� $��16(�5�      $
��Bold"�5�      ��5�   ,   �� *Title+5�      �� 5�   "   $
��0,10q�5�      $	 0.5Q�5�      ��5�   ,   �� *	Content+5�      5�      5�      5�      5�      5�      5�      5�      