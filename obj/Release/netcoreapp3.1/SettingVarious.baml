   M S B A M L   `   `   `  ���� F  BImageViewer, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null)clr-namespace:ImageViewerImageViewer       ImageViewer.SettingVarious   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     +localclr-namespace:ImageViewer     V RMaterialDesignThemes.Wpf, Version=3.1.3.1513, Culture=neutral, PublicKeyToken=nullZ VMaterialDesignThemes.MahApps, Version=0.1.4.1513, Culture=neutral, PublicKeyToken=nullHmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes      MaterialDesignBody#���     MaterialDesignPaper  N� 
Background#  �    MaterialDesignFont N� 
FontFamily# �   N� Title$ SettingSlide�� � 	Resources   SlideToBright �� 
TargetName �� TargetProperty �� From	 �� To g� Duration  SlideToBase  CloseToBright  CloseToBase��%�  &	       &	 a     &	 �     &	 #    �� .���� $ 	SlideIcon��$ Foreground.Color� 	#DDFFFFFF 	#DDFFFF4E$ 
00:00:00.3D��� .���� $ 	SlideIcon��$ Foreground.Color� 	#DDFFFF4E 	#DDFFFFFF$ 
00:00:00.3D��� .���� $ 	CloseIcon��$ Foreground.Color� 	#DDFFFFFF 	#DDFF1111$ 
00:00:00.3D��� .���� $ 	CloseIcon��$ Foreground.Color� 	#DDFF1111 	#DDFFFFFF$ 
00:00:00.3D�.��� 	 � RowDefinitions	 �� $��80 ��� $
��Auto �.+�(  "MaterialDesignThemes.Wpf.ColorZone  
   Mode$
 Custom=�$��10q�   CornerRadius$ 0��+  %MaterialDesignThemes.Wpf.ShadowAssist   ShadowDepth$ Depth3=�$��0q����	#FF673AB7.����  �� Orientation$ 
Horizontal=�.+��� -    * &MaterialDesignFloatingActionMiniButton#���  $ Depth0=����	#FF673AB7$��50��$��50�����	#FF673AB7$��0,0,0,0q�.��'  !MaterialDesignThemes.Wpf.PackIcon  -     Name$ 	SlideIcon��   Kind$ AnimationPlayOutline=�$��Center=�$	��Top=����	#DDFFFFFF$��50��$��40��$��Center=� � Triggers :�  :� RoutedEvent$ Button.MouseEnter��.[��� #��[ :� $ Button.MouseLeave��.[��� #��[ �� #���  $ Depth0=����	#FF673AB7$��50��$��50�����	#FF673AB7$��0,0,0,0q�.��  $ ShapeOutline=�$��Center=�$	��Top=����	#DDFFFF4E$��50��$��40��$��Center=��� -   #���  $ Depth0=����	#FF673AB7$��50��$��50�����	#FF673AB7$��-4,0,0,0q�.��  -   $ 	CloseIcon��$ CloseBoxOutline=�$��Center=�$	��Top=����	#DDFFFFFF$��50��$��38��$��Center=� :� $ Button.MouseEnter��.[��� #��[ :� $ Button.MouseLeave��.[��� #��[ �� -    ��Name$ SettingName��$��Various��
�������$	��220��$��44��$��36(�$��Center=� �� TextAlignment$ Right=�$
��Thin"�$��-3,0,0,0q�� $��1��$��
0,0,0,-210q� � ColumnDefinitions �� $��2* ��� $��1* �	 �� $��1* ��� $��1* ��� $��1* �.+��� $��0��$��0��$��
Background��
���   �$
��Auto��$��40��$��30(�$��Center=�$
 Left=�$
��Thin"�$��60,0,0,0q�$
��Left=��� -   $��0��$��1��-  'MaterialDesignThemes.Wpf.CheckBoxAssist   CheckBoxSize$ 35Q����	#FF673AB7���	#FF673AB7$��Center=�$��Center=�$��	-30,0,0,0q� d� 	IsChecked ��  �� Path$ check_iwndBackGrnd� �� Mode$ TwoWay=��� $��1��$��0��$��Volume��
���   �$
��Auto��$��40��$��30(�$��Center=�$
 Left=�$
��Thin"�$��60,0,0,0q�$
��Left=��� -   $��1��$��1��$ 35Q����	#FF673AB7���	#FF673AB7$��Center=�$��Center=�$��	-30,0,0,0q� �� $ check_Volume�$ TwoWay=��� $��2��$��0��$��Topmost��
���   �$
��Auto��$��40��$��30(�$��Center=�$
 Left=�$
��Thin"�$��60,0,0,0q�$
��Left=��� -   $��2��$��1��$ 35Q����	#FF673AB7���	#FF673AB7$��Center=�$��Center=�$��	-30,0,0,0q� �� $ check_tMost�$ TwoWay=�