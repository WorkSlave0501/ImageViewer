   M S B A M L   `   `   `  ����F  BImageViewer, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null)clr-namespace:ImageViewerImageViewer  5          ImageViewer.SettingSlide   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      +localclr-namespace:ImageViewer     5      V RMaterialDesignThemes.Wpf, Version=3.1.3.1513, Culture=neutral, PublicKeyToken=nullZ VMaterialDesignThemes.MahApps, Version=0.1.4.1513, Culture=neutral, PublicKeyToken=nullHmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes   5	         MaterialDesignBody#���   5
        MaterialDesignPaper  N� 
Background#  �  5        MaterialDesignFont N� 
FontFamily# �  5       N� Title$ SettingSlide��5       � 	Resources 5        VariousToBright �� 
TargetName �� TargetProperty �� From	 �� To g� Duration  VariousToBase  CloseToBright  CloseToBase��%�  &	       &	 �     &	 N    &	 �    �� 5   
   .��5      �� $ VariousIcon��5      $ Foreground.Color�5       	#DDFFFFFF5       	#DDFFFF4E6"   $ 
00:00:00.3D�5      5      �� 5   
   .��5      �� $ VariousIcon��5      $ Foreground.Color�5       	#DDFFFF4E5       	#DDFFFFFF6"   $ 
00:00:00.3D�5      5      �� 5#   
   .��5$      �� $ 	CloseIcon��5%      $ Foreground.Color�5&       	#DDFFFFFF5'       	#DDFF11116"   $ 
00:00:00.3D�5(      5)      �� 5+   
   .��5,      �� $ 	CloseIcon��5-      $ Foreground.Color�5.       	#DDFF11115/       	#DDFFFFFF6"   $ 
00:00:00.3D�50      51      52      .��54      � 	 � RowDefinitions	 55   
   �� 56      $��80 �6   �� 57      $
��Auto �6   .+�59   
   (  "MaterialDesignThemes.Wpf.ColorZone  
   Mode$
 Custom=�5:      $��10q�5;         CornerRadius$ 0��5<      +  %MaterialDesignThemes.Wpf.ShadowAssist   ShadowDepth$ Depth3=�5=      $��0q�5>      ���	#FF673AB7.��5?      ��  �� Orientation$ 
Horizontal=�6   .+�5A      ��  * &MaterialDesignFloatingActionMiniButton#���  6   $ Depth0=�5B      ���	#FF673AB7$��50��5C   0   $��50��6;   ���	#FF673AB7$��0,0,0,0q�6_   .��5D      '  !MaterialDesignThemes.Wpf.PackIcon  -   5          Name$ 	SlideIcon��5D   .      Kind$ AnimationPlayOutline=�6A   $��Center=�6]   $	��Top=�6x   ���	#DDFFFF4E$��50��6�   $��40��6�   $��Center=�6�   5E      �� 5G      -   5        #���  5G      $ Depth0=�5H      ���	#FF673AB7$��50��5I   0   $��50��6;   ���	#FF673AB7$��0,0,0,0q�6_   .��5K        -   5        $ VariousIcon��5K   .   $ ShapeOutline=�6C   $��Center=�6W   $	��Top=�6r   ���	#DDFFFFFF$��50��6�   $��40��6�   $��Center=�6�    � Triggers 5L      :� 5N       :� RoutedEvent$ Button.MouseEnter��6'   .[�5O      �� #��[ 6.   5P      :� 5R      $ Button.MouseLeave��6'   .[�5S      �� #��[ 6.   5T      5V      �� 5X      -   5        #���  5X      $ Depth0=�5Y      ���	#FF673AB7$��50��5Z   0   $��50��6;   ���	#FF673AB7$��-4,0,0,0q�6_   .��5\        -   5        $ 	CloseIcon��5\   .   $ CloseBoxOutline=�6A   $��Center=�6X   $	��Top=�6s   ���	#DDFFFFFF$��50��6�   $��38��6�   $��Center=�6�    5]      :� 5_      $ Button.MouseEnter��6'   .[�5`      �� #��[ 6.   5a      :� 5c      $ Button.MouseLeave��6'   .[�5d      �� #��[ 6.   5e      5g      �� 5i      -   5         ��Name$ SettingName��5i      $��	Slideshow��61   
�������$	��220��6U   $��44��6a   $��36(�6m   $��Center=�6{    �� TextAlignment$ Right=�6�   $
��Thin"�6�   $��-3,0,0,0q�6�   5k      5l      �� 5m   
   $��1��6   .+�5n      �� $��Interval��6   
���   �$
��Auto��6;   $��46��6H   $��22(�6T   $��Center=�6b   $
 Left=�6}   $
��Thin"�6�    �� Padding$ 	20,10,0,0q�6�   �� 5o      -   5         ��Name$ IntervalSlider��5o       �� TickFrequency$	 500Q�5p       �� IsSnapToTickEnabled$
 True��5q       �� IsMoveToPointEnabled$
 True��5r       �� Orientation$ 
Horizontal=�5s       �� TickPlacement$ BottomRight=�5t       � Minimum$
 1000Q�5u       � Maximum$ 10000Q�5v       � SmallChange$	 500Q�5x      ���	#FF673AB7$��Center=�5z      $	��300��5{       � Value 5w      ��  �� Path$ slideInterval�  �� Mode$  TwoWay=�5{      �� 5|      -   5        $ 
SlideValue��5|      $��Center=�6d   $��Center=�6�   $	��220��6�   $��80��6�   $��72(�6�   $
��Thin"�6�   $ Center=�6�   $��0,10,0,0q�6�   ��6,   �� ! �� ElementName$! IntervalSlider��$ Value�6�   �� 5}      $
��msec��6   $��Center=�6$   $��Center=�6A   $	��120��6\   $��30��6h   $��22(�6u   $
��Thin"�6�   $ Center=�6�   $��0,0,0,0q�6�   5~      5      5�      