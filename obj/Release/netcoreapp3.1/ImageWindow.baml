   M S B A M L   `   `   `  ����   ImageViewer    ImageViewer.ImageWindow   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     i eHardcodet.Wpf.TaskbarNotification, Version=1.0.13.0, Culture=neutral, PublicKeyToken=b9e9fbe9b6908adc)tb http://www.hardcodet.net/taskbar  U QWpfAnimatedGif, Version=2.0.0.0, Culture=neutral, PublicKeyToken=9e7cd3b544a090dc,gif"http://wpfanimatedgif.codeplex.com  -     �Name$  iWindow�� � Title$ ImageViewer��$	��240��$	��135�� � Left$	 440��
 � Top$	 250�� J� Opacity$ 0Q� � ShowInTaskbar$ False�� � WindowStyle$
 None=� � AllowsTransparency$
 True�� � 
ResizeMode$ CanResizeWithGrip=�	 J� 	AllowDrop$
	 True������ 
 �� Path$
 iwndBackGrnd� �� Mode$ OneWay=� � Topmost �� $
 tMost�$ OneWay=� � 	Resources    IndicateBarAnime �� 
TargetName �� TargetProperty W� From	 W� To g� Duration  HideBarAnime  IndicateWindow  
HideWindow  SliderBackgroundColorbrush �� Color  SliderForegroundColorBrush  SliderThumbColorBrush  CustomSliderThumbStyle �� 
TargetType �� Property �� Value �� 
TargetType �� CornerRadius $  CustomSliderDecreaseRepeatButton $	  CustomSliderIncreaseRepeatButton 
 CustomSliderStyle �� Triggers P� Property P� Value � RowDefinitions ]�Name ]� DecreaseRepeatButton ]� IncreaseRepeatButton  ]� Thumb��%O  &	        &	 F     &	 �     &	 �     &	     &	 -    &	 B    &	 W    7  &	     &		 �    &	
 �    7  7 	 7  �� .��W� $ SeekBar��$ Opacity� 0 1$ 
00:00:00.3D��� .��W� $ SeekBar��$ Opacity� 1 0$ 
00:00:00.3D��� .��W� $ iWindow��$ Opacity� 0 1$ 
00:00:00.2D��� .��W� $ iWindow��$ Opacity� 1 0$ 
00:00:00.2D��� $ 	#FF222222���� $ 	#FFAAAAAA���� $ 	#FF673AB7���� " n�.���  �@J�SnapsToDevicePixels$
 True����  �@�OverridesDefaultStyle$
 True����  � �� �� " n�.R��� $��12��$��6��$ 2��8��  $��-5, 0, -5, 0q��� " ��.���  �@J�SnapsToDevicePixels$
 True����  �@�OverridesDefaultStyle$
 True����  � ��$ False����  � |�$ False����  � �� �� " ��.R��� $ 2,2,2,2��$��0q�#���  $��-5, 0, -5, 0q��� " ��.���  �@J�SnapsToDevicePixels$
 True����  �@�OverridesDefaultStyle$
 True����  � ��$ False����  � |�$ False����  � �� �� " ��.R��� $ 0,2,2,0��$��0q�#���  $��-5, 0, -5, 0q��� " ��.���  � |�$ False����  �@J�SnapsToDevicePixels$
 True����  �@�OverridesDefaultStyle$
 True����  � ��$ 15, 0, 15, 0q� P�  �@��Orientation$ 
Horizontal=�. ���  � �� �� .R��  �� $
��Auto ��� $��25 ��� $
��Auto �.+��� $��1��$��4��
������ $��Center=�$ 2��]� $ 
PART_Track��$��1�� �� 8��  $��Slider.DecreaseLarge��$��6�� �� 8�� $��Slider.IncreaseLarge��$��6��  n� 8�� ! � Triggers! :� " :� RoutedEvent$" Window.MouseEnter��.[��� #��[  :� $" Window.MouseLeave��.[��� #��[ .��� -   # �Name$# 
GridEntity��
������ $ J� RenderTransformOrigin$$ 0.5,0.5&� �� $��4* ��� $��1* �% � ColumnDefinitions% �� $��1* ��� $��3* ��� $��1* �z��� & �� ScaleX& �� $
 
scaleValue�$ OneWay=�' �� ScaleY' �� $
 
scaleValue�$ OneWay=�.+�r� -   ( r�Name$( movie��$��3��$��2��$
	 True��) r� LoadedBehavior$) Manual=�* r� Volume* �� $
 Volume�$ OneWay=��� -   + ��Name$+ img_gif��$��3��$
	 True��$��2��"  WpfAnimatedGif.ImageBehavior,   AnimatedSource, �� $
 gifPath�$ OneWay=��� -   $	+ img��$��3��$
	 True��$��2���� -   - ��Name$- 	leftField��
������ $��0��$
	 True��$��2���� -   $- 
rightField��
������ $��2��$
	 True��$��2���� -   $��1��$��2��.���� -	   $- middleField��
������ $��1��$
	 True���� -
   . ��Name$. SeekBar��$��3��$��1��/ �� IsMoveToPointEnabled$
/ True��$��Center=�0 � Minimum$0 0Q�1 � Maximum$	1 100Q�$ 0Q�#��� 
 