   M S B A M L   `   `   `  ����  ImageViewer    ImageViewer.ImageWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   i eHardcodet.Wpf.TaskbarNotification, Version=1.0.13.0, Culture=neutral, PublicKeyToken=b9e9fbe9b6908adc)tb http://www.hardcodet.net/taskbar  5   	   U QWpfAnimatedGif, Version=2.0.0.0, Culture=neutral, PublicKeyToken=9e7cd3b544a090dc,gif"http://wpfanimatedgif.codeplex.com  5   	   -   5          �Name$  iWindow��5   	    � Title$ ImageViewer��5
   	   $	��240��5   	   $	��135��6    � Left$	 440��5   	   
 � Top$	 250��6    J� Opacity$ 0Q�5   	    � ShowInTaskbar$ False��5   	    � WindowStyle$
 None=�6    � AllowsTransparency$
 True��5   	    � 
ResizeMode$ CanResizeWithGrip=�5   	   	 J� 	AllowDrop$
	 True��5   	   ��5   	   �� 
 �� Path$
 iwndBackGrnd� �� Mode$ OneWay=� � Topmost 5   	   �� $
 tMost�$ OneWay=� � 	Resources 5         IndicateBarAnime �� 
TargetName �� TargetProperty W� From	 W� To g� Duration  HideBarAnime  IndicateWindow  
HideWindow  SliderBackgroundColorbrush �� Color  SliderForegroundColorBrush  SliderThumbColorBrush  CustomSliderThumbStyle �� 
TargetType �� Property �� Value �� 
TargetType �� CornerRadius $  CustomSliderDecreaseRepeatButton $	  CustomSliderIncreaseRepeatButton 
 CustomSliderStyle �� Triggers P� Property P� Value � RowDefinitions ]�Name ]� DecreaseRepeatButton ]� IncreaseRepeatButton  ]� Thumb��%�
  &	        &	 �     &	     &	 �    &	 (    &	 K    &	 n    &	 �    7  &	 �    &		 d    &	
 �    7  7 	 7  �� 5   
   .��5       W� $ SeekBar��5!      $ Opacity�5"       05#       16   $ 
00:00:00.3D�5$      5%      �� 5'   
   .��5(      W� $ SeekBar��5)      $ Opacity�5*       15+       06   $ 
00:00:00.3D�5,      5-      �� 50   
   .��51      W� $ iWindow��52      $ Opacity�53       054       16   $ 
00:00:00.2D�55      56      �� 58   
   .��59      W� $ iWindow��5:      $ Opacity�5;       15<       06   $ 
00:00:00.2D�5=      5>      �� 5B   
   $ 	#FF222222��6=   �� 5C   
   $ 	#FFAAAAAA��6=   �� 5D   
   $ 	#FF673AB7��68   �� 5G   
   " n�6/   .�5H      ��  �@J�SnapsToDevicePixels$
 True��64   �� 5I       �@�OverridesDefaultStyle$
 True��66   �� 5J       � �� 5K      �� 5L      " n�6&   .R�5M      �� $��12��6!   $��6��6,   $ 2��67   8��  $��-5, 0, -5, 0q�5O   !   5P      5R      5S      �� 5V   
   " ��69   .�5W      ��  �@J�SnapsToDevicePixels$
 True��64   �� 5X       �@�OverridesDefaultStyle$
 True��66   �� 5Y       � ��$ False��6*   �� 5Z       � |�$ False��6*   �� 5[       � �� 5\      �� 5]      " ��6&   .R�5^      �� $ 2,2,2,2��6!   $��0q�68   #���  5_   !   $��-5, 0, -5, 0q�5`   !   5a      5c      5d      �� 5g   
   " ��69   .�5h      ��  �@J�SnapsToDevicePixels$
 True��64   �� 5i       �@�OverridesDefaultStyle$
 True��66   �� 5j       � ��$ False��6*   �� 5k       � |�$ False��6*   �� 5l       � �� 5m      �� 5n      " ��6&   .R�5o      �� $ 0,2,2,0��6!   $��0q�68   #���  5p   !   $��-5, 0, -5, 0q�5q   !   5r      5t      5u      �� 5x   
   " ��6*   .�5y      ��  � |�$ False��6*   �� 5z       �@J�SnapsToDevicePixels$
 True��64   �� 5{       �@�OverridesDefaultStyle$
 True��66   �� 5|       � ��$ 15, 0, 15, 0q�6'    5}      P� 5~       �@��Orientation$ 
Horizontal=�61   . �5�      ��  � �� 5�      �� 5�      .R�5�   "   �  5�   &   �� 5�   *   $
��Auto �68   �� 5�   *   $��25 �68   �� 5�   *   $
��Auto �68   .+�5�   &   �� $��1��6-   $��4��6:   
������ $��Center=�6^   $ 2��6y   ]� 5�   &   $ 
PART_Track��69   $��1��6,    5�   *   �� 5�   .   8��  $��Slider.DecreaseLarge��6u   $��6��6�    5�   *   �� 5�   .   8�� $��Slider.IncreaseLarge��6u   $��6��6�     5�   *   n� 5�   .   8�� 64   5�   '   5�   #   5�      5�      5�      5�      5�      ! � Triggers! 5�      :� 5�   
   " :� RoutedEvent$" Window.MouseEnter��6   .[�5�      �� #��[  6   5�      :� 5�   
   $" Window.MouseLeave��6   .[�5�      �� #��[ 6   5�      .��5�      � -   5        # �Name$# 
GridEntity��5�      
������ $ J� RenderTransformOrigin$$ 0.5,0.5&�5�       5�   
   �� 5�      $��4* �6   �� 5�      $��1* �6   % � ColumnDefinitions% 5�   
   �� 5�      $��1* �6   �� 5�      $��3* �6   �� 5�      $��1* �6   z�5�   
   �� 5�      & �� ScaleX& 6   �� $
 
scaleValue�$ OneWay=�' �� ScaleY' 5�      �� $
 
scaleValue�$ OneWay=�.+�5�   
   r� -   5        ( r�Name$( movie��5�      $��3��6&   $��2��6:   $
	 True��5�      ) r� LoadedBehavior$) Manual=�5�      * r� Volume* 5�      �� $
 Volume�$ OneWay=�5�      �� 5�   
   -   5        + ��Name$+ img_gif��5�      $��3��6!   $
	 True��5�      $��2��6!   "  WpfAnimatedGif.ImageBehavior,   AnimatedSource, 5�      �� $
 gifPath�$ OneWay=�5�   !   �� 5�   
   -   5        $	+ img��5�      $��3��6   $
	 True��61   $��2��6B   �� 5�   
   -   5        - ��Name$- 	leftField��5�      
������ $��0��6:   $
	 True��5�   =   $��2��6N   �� 5�   
   -   5        $- 
rightField��5�      
������ $��2��6;   $
	 True��5�   >   $��2��6O   �� 5�   
   -   5        $��1��5�   =   $��2��6M   .��5�      �� -	   5        $- middleField��5�      
������ $��1��6@   $
	 True��5�      5�      �� 5�   
   -
   5        . ��Name$. SeekBar��5�      $��3��5�      $��1��5�      / �� IsMoveToPointEnabled$
/ True��5�      $��Center=�5�      0 � Minimum$0 0Q�5�      1 � Maximum$	1 100Q�6   $ 0Q�5�      #��� 
 5�      5�      5�      5�      