   M S B A M L   `   `   `  ���� F  BImageViewer, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null)clr-namespace:ImageViewerImageViewer      ImageViewer.SettingSlide   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     +localclr-namespace:ImageViewer     V RMaterialDesignThemes.Wpf, Version=3.1.3.1513, Culture=neutral, PublicKeyToken=nullZ VMaterialDesignThemes.MahApps, Version=0.1.4.1513, Culture=neutral, PublicKeyToken=nullHmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes      MaterialDesignBody#���     MaterialDesignPaper  N� 
Background#  �    MaterialDesignFont N� 
FontFamily# �   N� Title$ SettingSlide�� � 	Resources   VariousToBright �� 
TargetName �� TargetProperty �� From	 �� To g� Duration  VariousToBase  CloseToBright  CloseToBase��%�  &	       &	 c     &	 �     &	 '    �� .���� $ VariousIcon��$ Foreground.Color� 	#DDFFFFFF 	#DDFFFF4E$ 
00:00:00.3D��� .���� $ VariousIcon��$ Foreground.Color� 	#DDFFFF4E 	#DDFFFFFF$ 
00:00:00.3D��� .���� $ 	CloseIcon��$ Foreground.Color� 	#DDFFFFFF 	#DDFF1111$ 
00:00:00.3D��� .���� $ 	CloseIcon��$ Foreground.Color� 	#DDFF1111 	#DDFFFFFF$ 
00:00:00.3D�.��� 	 � RowDefinitions	 �� $��80 ��� $
��Auto �.+�(  "MaterialDesignThemes.Wpf.ColorZone  
   Mode$
 Custom=�$��10q�   CornerRadius$ 0��+  %MaterialDesignThemes.Wpf.ShadowAssist   ShadowDepth$ Depth3=�$��0q����	#FF673AB7.����  �� Orientation$ 
Horizontal=�.+���  * &MaterialDesignFloatingActionMiniButton#���  $ Depth0=����	#FF673AB7$��50��$��50�����	#FF673AB7$��0,0,0,0q�.��'  !MaterialDesignThemes.Wpf.PackIcon  -     Name$ 	SlideIcon��   Kind$ AnimationPlayOutline=�$��Center=�$	��Top=����	#DDFFFF4E$��50��$��40��$��Center=��� -   #���  $ Depth0=����	#FF673AB7$��50��$��50�����	#FF673AB7$��0,0,0,0q�.��  -   $ VariousIcon��$ ShapeOutline=�$��Center=�$	��Top=����	#DDFFFFFF$��50��$��40��$��Center=� � Triggers :�  :� RoutedEvent$ Button.MouseEnter��.[��� #��[ :� $ Button.MouseLeave��.[��� #��[ �� -   #���  $ Depth0=����	#FF673AB7$��50��$��50�����	#FF673AB7$��-4,0,0,0q�.��  -   $ 	CloseIcon��$ CloseBoxOutline=�$��Center=�$	��Top=����	#DDFFFFFF$��50��$��38��$��Center=� :� $ Button.MouseEnter��.[��� #��[ :� $ Button.MouseLeave��.[��� #��[ �� -    ��Name$ SettingName��$��	Slideshow��
�������$	��220��$��44��$��36(�$��Center=� �� TextAlignment$ Right=�$
��Thin"�$��-3,0,0,0q��� $��1��.+��� $��Interval��
���   �$
��Auto��$��46��$��22(�$��Center=�$
 Left=�$
��Thin"� �� Padding$ 	20,10,0,0q��� -    ��Name$ IntervalSlider�� �� TickFrequency$	 500Q� �� IsSnapToTickEnabled$
 True�� �� IsMoveToPointEnabled$
 True�� �� Orientation$ 
Horizontal=� �� TickPlacement$ BottomRight=� � Minimum$
 1000Q� � Maximum$ 10000Q� � SmallChange$	 500Q����	#FF673AB7$��Center=�$	��300�� � Value ��  �� Path$ slideInterval�  �� Mode$  TwoWay=��� -   $ 
SlideValue��$��Center=�$��Center=�$	��220��$��80��$��72(�$
��Thin"�$ Center=�$��0,10,0,0q����� ! �� ElementName$! IntervalSlider��$ Value��� $
��msec��$��Center=�$��Center=�$	��120��$��30��$��22(�$
��Thin"�$ Center=�$��0,0,0,0q�