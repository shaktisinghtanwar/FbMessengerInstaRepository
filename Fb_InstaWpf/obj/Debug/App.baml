   M S B A M L   `   `   `  ����F  BFb_InstaWpf, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null?$clr-namespace:Fb_InstaWpf.ConvertersFb_InstaWpf.Converters  5   	   7 clr-namespace:Fb_InstaWpf.HelperFb_InstaWpf.Helper  5   	       Fb_InstaWpf.App   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      ;
converters$clr-namespace:Fb_InstaWpf.Converters     5   	   3helper clr-namespace:Fb_InstaWpf.Helper     5   	     �� 	Resources  5       �� MergedDictionaries �� Source   PrimaryBrush �� Color  PrimaryFont0   *Fb_InstaWpf.Converters.AddValuesWithHyhpen  AddValuesWithHyhpen  MyStyle �� 
TargetType �� Property �� Value �� 
TargetType �� Triggers	 P� Property
 P� Value  LoginUserDataTemplate �� CornerRadius �� Orientation �� TextAlignment  OtherUserDataTemplate  OtherimgDataTemplate  LoginimgDataTemplate  TimeDataTemplate4   .Fb_InstaWpf.Helper.MessageDataTemplateSelector 	 MessageDataTemplateSelector 
 LoginUserTemplateForFbCmnt  OtherUserTemplateForFbCmnt  OtherimgForFbCmnt  LoginimgForFbCmnt;   5Fb_InstaWpf.Helper.MessageFBComntDataTemplateSelector & "MessageFBComntDataTemplateSelector  OtherimgForInstaCmnt  LoginimgForInstaCmnt ! LoginUserTemplateForInstaCmnt ! OtherUserTemplateForInstaCmnt>   8Fb_InstaWpf.Helper.MessageInstaComntDataTemplateSelector ) %MessageInstaComntDataTemplateSelector�� 5	   
    5
      �� 5      $ Themes\TabControl.xaml?�6%   �� 5      $ Themes\Button.xaml?�6%   �� 5      $ Themes\GroupBox.xaml?�6%   �� 5      $ Themes\ComboBox.xaml?�6%   �� 5      $ Themes\TreeView.xaml?�6%   �� 5      $ Themes\Menu.xaml?�6%   �� 5      $ Themes\ProgressBar.xaml?�6%   �� 5      $ Themes\ToolTip.xaml?�6%   %4  &	        &	 #     &	 B     &	 U     &	 �    &	 �    &	 1    &	     &	     &		     &	
 0    &	 6	    &	 <
    &	 8    &	 5    &	 H    &	 G    &	 G    &	 P    &	 Y    �� 5   
   $ 	#FF4169AA��6/   �� 5   
   $ White��6.     5   
   6)   �� 5   
   " ��6    .�5      ��  � �� �	#FFA1A7C06+   �� 5       � �� 5       �� 5!      " ��6&   .R�5"      �� #��z��6!   
���   �$��1q�6_   ._�5#      �� $��Center=�6/   $��Center=�6L   5$      5%      5'       5(      P� 5)      	 �@J�IsMouseOver
 . . �5*      ��  � ��
 ����6H   �� 5+       � ��$
 Bold"�63   6B   5,      5.      �� 5/   
   .R�50      � 
������ $
��auto��60   $
��Left=�6=   .+�51      �� $
��Left=�6   
�������$ 20��6M   ._�52      �� $ 
Horizontal=�6!   .+�53      �� $
��auto��6H   $
��Left=�6X   ��6$   �� *loginguserimage+6u   �� 54      |�.  $
��auto��67   $��10q�6D   $
 Left=�6j   $
��Wrap=�6   ��6P   �� *	Message+6�   55      56      57      58      �� 59   
   .R�5:      � 
������ $
��auto��60   $��Right=�6=   .+�5;      �� $��Right=�6   
����iA�$ 20��6N   ._�5<      �� $ 
Horizontal=�6!   .+�5=      �� $
��auto��6D   $��Right=�6R   ��6"   �� *otheruserimage+6p   �� 5>      $
��auto��6&   $��10q�6L   
�������$ Right=�6n   $
��Wrap=�6�   ��63   �� *	Message+6�   5?      5@      5A      5B      �� 5C   
   .R�5D      � 
������ $
��auto��6,   $
��Left=�69   .+�5E      �� $
��Left=�6   
������ ._�5F      �� $	��250��6@   $
��Left=�6N   ��6   �� *otheruserimage+6k   5G      5H      5I      �� 5J   
   .R�5K      � 
������ $
��auto��6,   $��Right=�69   .+�5L      �� $��Right=�6   
������ ._�5M      �� $	��250��6@   $��Right=�6N   ��6   �� *loginguserimage+6l   5N      5O      5P      �� 5Q   
   .R�5R      � 
������ $	��400��6,   $��Center=�68   .+�5S      �� $��Center=�6   
������ $ 20��6Q   ._�5T      �� $
��auto��6!   $��10q�6D   
���   �$ Center=�6d   $
��Wrap=�6{   ��6.   �� *Time+6�   5U      5V      5W        5X   
   6W   �� 5Y   
   .R�5Z      � .+�5[      �� $��1q�6   
�������
�������$ 20��6^   ._�5\      �� $��Center=�6    
���   �$
��auto��6i   
�������$ Center=�6�   $
��Wrap=�6�   ��6=   �� *	Message+6�   5]      5^      5_      �� 5`   
   .R�5a      � .+�5b      �� $��1q�6   
�������
�������$ 20��6`   ._�5c      �� $��Center=�6    
���   �$
��auto��6j   
�������$ Center=�6�   $
��Wrap=�6�   ��6=   �� *	Message+6�   5d      5e      5g      �� 5i   
   .R�5j      � 
������ $
��auto��6,   $��Center=�69   .+�5k      �� $��Center=�6   
������ ._�5l      �� $��50��6A   $��50��6L   $��Center=�6X   ��6   �� *otheruserFbimage+6w   5m      5n      5o      �� 5r   
   .R�5s      � 
������ $
��auto��6,   $��Center=�69   .+�5t      �� $��Center=�6   
������ ._�5u      �� $��60��6B   $��60��6M   $��Center=�6Y   ��6   �� *loginguserFbimage+6x   5v      5w      5x        5z   
   6a   �� 5|   
   .R�5}      � 
������ $
��auto��6,   $��Center=�69   .+�5~      �� $��Center=�6   
������ ._�5      �� $��50��6D   $��50��6O   $��Center=�6[   ��6   �� *otheruserInstaimage+6z   5�      5�      5�      �� 5�   
   .R�5�      � 
������ $
��auto��6,   $��Center=�69   .+�5�      �� $��Center=�6   
������ ._�5�      �� $��40��6E   $��40��6P   $��Center=�6\   ��6   �� *loginguserInstaimage+6{   5�      5�      5�      �� 5�   
   .R�5�      � .+�5�      �� $��1q�6   
�������
�������$ 20��6^   ._�5�      �� $��Center=�6    $
��auto��6>   $��10q�6e   
�������$ Center=�6�   $
��Wrap=�6�   ��6L   �� *	Message+6�   5�      5�      5�      �� 5�   
   .R�5�      � .+�5�      �� $��1q�6   
�������
�������$ 20��6`   ._�5�      �� $��Center=�6    $
��auto��6V   $��10q�6e   
�������$ Center=�6�   $
��Wrap=�6�   ��6=   �� *	Message+6�   5�      5�      5�        5�   
   6g   5�      5�      