   M S B A M L   `   `   `  ����F  BImageViewer, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null)clr-namespace:ImageViewerImageViewer  5           ImageViewer.SettingVarious   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      +localclr-namespace:ImageViewer     5      V RMaterialDesignThemes.Wpf, Version=3.1.3.1513, Culture=neutral, PublicKeyToken=nullZ VMaterialDesignThemes.MahApps, Version=0.1.4.1513, Culture=neutral, PublicKeyToken=nullHmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes   5	         MaterialDesignBody#���   5
        MaterialDesignPaper  N� 
Background#  �  5        MaterialDesignFont N� 
FontFamily# �  5       N� Title$ SettingSlide��5       � 	Resources 5        SlideToBright �� 
TargetName �� TargetProperty �� From	 �� To g� Duration  SlideToBase  CloseToBright  CloseToBase��%�  &	       &	 �     &	 J    &	 �    �� 5   
   .��5      �� $ 	SlideIcon��5      $ Foreground.Color�5       	#DDFFFFFF5       	#DDFFFF4E6"   $ 
00:00:00.3D�5      5      �� 5   
   .��5      �� $ 	SlideIcon��5      $ Foreground.Color�5       	#DDFFFF4E5       	#DDFFFFFF6"   $ 
00:00:00.3D�5      5       �� 5$   
   .��5%      �� $ 	CloseIcon��5&      $ Foreground.Color�5'       	#DDFFFFFF5(       	#DDFF11116"   $ 
00:00:00.3D�5)      5*      �� 5,   
   .��5-      �� $ 	CloseIcon��5.      $ Foreground.Color�5/       	#DDFF111150       	#DDFFFFFF6"   $ 
00:00:00.3D�51      52      53      .��55      � 	 � RowDefinitions	 56   
   �� 57      $��80 �6   �� 58      $
��Auto �6   .+�5:   
   (  "MaterialDesignThemes.Wpf.ColorZone  
   Mode$
 Custom=�5;      $��10q�5<         CornerRadius$ 0��5=      +  %MaterialDesignThemes.Wpf.ShadowAssist   ShadowDepth$ Depth3=�5>      $��0q�5?      ���	#FF673AB7.��5@      ��  �� Orientation$ 
Horizontal=�6   .+�5A      �� -   5         * &MaterialDesignFloatingActionMiniButton#���  5A      $ Depth0=�5B      ���	#FF673AB7$��50��5C   0   $��50��6;   ���	#FF673AB7$��0,0,0,0q�6_   .��5E      '  !MaterialDesignThemes.Wpf.PackIcon  -   5          Name$ 	SlideIcon��5E   .      Kind$ AnimationPlayOutline=�6A   $��Center=�6]   $	��Top=�6x   ���	#DDFFFFFF$��50��6�   $��40��6�   $��Center=�6�    � Triggers 5F      :� 5H       :� RoutedEvent$ Button.MouseEnter��6'   .[�5I      �� #��[ 6.   5J      :� 5L      $ Button.MouseLeave��6'   .[�5M      �� #��[ 6.   5N      5P      �� 5Q      #���  6   $ Depth0=�5R      ���	#FF673AB7$��50��5S   0   $��50��6;   ���	#FF673AB7$��0,0,0,0q�6_   .��5T        $ ShapeOutline=�6.   $��Center=�6B   $	��Top=�6]   ���	#DDFFFF4E$��50��6�   $��40��6�   $��Center=�6�   5U      �� 5V      -   5        #���  5V      $ Depth0=�5W      ���	#FF673AB7$��50��5X   0   $��50��6;   ���	#FF673AB7$��-4,0,0,0q�6_   .��5Z        -   5        $ 	CloseIcon��5Z   .   $ CloseBoxOutline=�6A   $��Center=�6X   $	��Top=�6s   ���	#DDFFFFFF$��50��6�   $��38��6�   $��Center=�6�    5[      :� 5]      $ Button.MouseEnter��6'   .[�5^      �� #��[ 6.   5_      :� 5a      $ Button.MouseLeave��6'   .[�5b      �� #��[ 6.   5c      5e      �� 5f      -   5         ��Name$ SettingName��5f      $��Various��61   
�������$	��220��6S   $��44��6_   $��36(�6k   $��Center=�6y    �� TextAlignment$ Right=�6�   $
��Thin"�6�   $��-3,0,0,0q�6�   5g      5h      � 5i   
   $��1��6   $��
0,0,0,-210q�6    � ColumnDefinitions 5j      �� 5k      $��2* �6#   �� 5l      $��1* �6#   	 5n      �� 5o      $��1* �6    �� 5p      $��1* �6    �� 5q      $��1* �6    .+�5s      �� $��0��6   $��0��6%   $��
Background��65   
���   �$
��Auto��6Z   $��40��6g   $��30(�6s   $��Center=�6�   $
 Left=�6�   $
��Thin"�6�   $��60,0,0,0q�6�   $
��Left=�6�   �� 5t      -   5        $��0��5u      $��1��6   -  'MaterialDesignThemes.Wpf.CheckBoxAssist   CheckBoxSize$ 35Q�5w      ���	#FF673AB7���	#FF673AB7$��Center=�5y      $��Center=�6*   $��	-30,0,0,0q�5z       d� 	IsChecked 5v      ��  �� Path$ check_iwndBackGrnd� �� Mode$ TwoWay=�5|      �� 5~      $��1��6   $��0��6%   $��Volume��65   
���   �$
��Auto��6V   $��40��6c   $��30(�6o   $��Center=�6}   $
 Left=�6�   $
��Thin"�6�   $��60,0,0,0q�6�   $
��Left=�6�   �� 5      -   5        $��1��5�      $��1��6   $ 35Q�5�      ���	#FF673AB7���	#FF673AB7$��Center=�5�      $��Center=�6*   $��	-30,0,0,0q�5�       5�      �� $ check_Volume�$ TwoWay=�5�   7   �� 5�      $��2��6   $��0��6%   $��Topmost��65   
���   �$
��Auto��6W   $��40��6d   $��30(�6p   $��Center=�6~   $
 Left=�6�   $
��Thin"�6�   $��60,0,0,0q�6�   $
��Left=�6�   �� 5�      -   5        $��2��5�      $��1��6   $ 35Q�5�      ���	#FF673AB7���	#FF673AB7$��Center=�5�      $��Center=�6*   $��	-30,0,0,0q�5�       5�      �� $ check_tMost�$ TwoWay=�5�   9   5�      5�      5�      