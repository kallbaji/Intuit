   M S B A M L   `   `   `  ����@  <iTube, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullclr-namespace:iTubeiTube  5      E ADragablz, Version=0.0.3.234, Culture=neutral, PublicKeyToken=null5(clr-namespace:Dragablz;assembly=DragablzDragablz 5          	iTube.App   5      W SPresentationCore, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      %localclr-namespace:iTube     5      =dragablz(clr-namespace:Dragablz;assembly=Dragablz     5        �� 	Resources  5       �� MergedDictionaries �� Source   Key �� Color  Primary100Foreground  Primary500Foreground  Primary700Foreground  Accent200Foreground  Dragablz.TabablzControl �� 
TargetType % !MaterialDesignTabablzControlStyle �� BasedOn  SliderRepeatButton �� Property �� Value �� 
TargetType  SliderRepeatButton1	 J� SnapsToDevicePixels  SliderThumb 
	 Slider
 � RowDefinitions ]�Name ]� DecreaseRepeatButton ]� Thumb ]� IncreaseRepeatButton 
 SliderStyle �� Triggers P� Property P� Value�� 5   
    5	      �� 5       5      �� 5      $b \pack://application:,,,/MaterialDesignColors;component/Themes/MaterialDesignColor.purple.xaml?�6-   �� 5      PrimaryHueLightBrush  $ White��6C   �� 5      "PrimaryHueLightForegroundBrush  # [ 6M   �� 5      PrimaryHueMidBrush  $	 Red��6A   �� 5       PrimaryHueMidForegroundBrush  # [ 6K   �� 5      PrimaryHueDarkBrush  $ DimGray��6B   �� 5      !PrimaryHueDarkForegroundBrush  # [ 6L   5      �� 5       5       �� 5!      $b \pack://application:,,,/MaterialDesignColors;component/Themes/MaterialDesignColor.purple.xaml?�6-   �� 5%      SecondaryAccentBrush  $ White��6C   �� 5&      "SecondaryAccentForegroundBrush  # [ 6M   5'      �� 5*      $J Dpack://application:,,,/Dragablz;component/Themes/materialdesign.xaml?�6%   �� 5+      $h bpack://application:,,,/MaterialDesignThemes.Wpf;component/Themes/MaterialDesignTheme.Defaults.xaml?�6%   %O  '        7  &	 "     &	 �    &	 �    &		 �    7  7  7  &	
 �    7 	 �� 5/      "  6   8   6B   �� 50      " ��6/   .�51      ��  �@J�SnapsToDevicePixels$
 true��68   �� 52       �@�OverridesDefaultStyle$
 true��6:   �� 53       � ��$ false��6.   �� 54       � |�$ false��6.   �� 55       � �� 56      �� 57      " ��6*   .R�58      �� $��1q�6&   
�������
�������$��3��6_   59      5;      5<      �� 5>      " ��60   .�5?      ��  �@J�SnapsToDevicePixels$
 true��68   �� 5@       �@�OverridesDefaultStyle$
 true��6:   �� 5A       � �� 5B      �� 5C      " ��6*   .R�5D      �� $
	 True��6%   
���  ��$��1q�6R   
���  ��$��3��6x   5E      5G      5H      �� 5J      " n�6(   .�5K      ��  �@J�SnapsToDevicePixels$
 true��68   �� 5L       �@�OverridesDefaultStyle$
 true��6:   �� 5M       � �� 5N      �� 5O      " n�6*   .R�5P      A� $��10��6&   $��10��62   
���  ��6J   5Q      5S      5T      �� 5V      " ��6.   .R�5W      � 
 5X      �� 5Y      $
��Auto �6(   �� 5Z      $
��Auto �6(   #��z��66   �� 5[      $
��Auto �6(   .+�5]      ]� $ 
PART_Track��6)   $��1��6    5^      �� 5_      8��  $��Slider.DecreaseLarge��6Y    5a      n� 5b      8�� 6$    5d      �� 5e      8�� $��Slider.IncreaseLarge��6W   5g      5h      5i      �� 5k      " ��6(   .�5l      ��  � |�$ False��6.   �� 5m       �@J�SnapsToDevicePixels$
 true��68   �� 5n       �@�OverridesDefaultStyle$
 true��6:    5o      P� 5p       �@��Orientation$ 
Horizontal=�65   . �5q      ��  � ��$ 21��66   �� 5r       � ��$	 104��65   �� 5s       � ��8   65   5t      5v      5x      5{      