   M S B A M L   `   `   `  ����F  BFb_InstaWpf, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null3clr-namespace:Fb_InstaWpf.ViewFb_InstaWpf.View  5      5clr-namespace:Fb_InstaWpf.ModelFb_InstaWpf.Model  5      -    'Fb_InstaWpf.View.SocialUserMessagesView   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      0localclr-namespace:Fb_InstaWpf.View     5      1modelclr-namespace:Fb_InstaWpf.Model     5      .��5
      �   � RowDefinitions  5   
   �� 5      $��* �6   6+   �� 5      $	��100 �6   .+�5   
   � .+�5      �� ���	#FFDEDEDE �� SelectedItem 6   ��  �� Path$ ActiveTabUser���6E   �� *SelectedUsers+ �� Mode$ TwoWay=���5      �� 5       �� DataType$ model:UserMsgTabItem��6#   .R�5      ��  �� Orientation$ 
Horizontal=�6%   .+�5      �� ��6(   �� *InboxUserName+�� 5      ��6%   ��  �� RelativeSource ��  �� AncestorType$ WindowL�$! DataContext.CloseTabCommand���5   "   `� 5   &      AddValuesWithHyhpen `� 	Converter# [  63   .1�5   *   �� $ InboxUserName�62   �� 5   *   $ MessageUserType�62   5   '   .��5   "   �� $��10��6(   $��10��64   $=��7/Fb_InstaWpf;component/Images/if_cross-button_45933.png��6?   5      5      5!      	 �� ContentTemplate	 5$      �� 5%      $ model:UserMsgTabItem��6#   .R�5&      ��   MessageDataTemplateSelector#��[ 6"   $
��Left=�6f   $��Disabled=�5'   [   ��6:   �� *
Messages+��5(      �� 5)   "   
 �� 
TargetType"
 ��6(   .�5*   &   ��  �� Property � �� �� Value$ Stretch=�6S   �� 5+   &    � ��$	 942��6>   5,   #   5/      50      52      54      � 56   
   $��1��6     PrimaryBrush#���  6    � ColumnDefinitions 57      �� 58      6&   �� 59      $	��178 �6#   61   .+�5;      �  5<      �� 5=      $	��80* �6'   65   �� 5>      6)   .+�5@      ]� $��0��6   .+�5B      �� -   5         ��Name$ 
msgtxtbox2��5B       �� TextWrapping$
 Wrap=�62   $	��800��6G   #��� ��6|   $
��Left=�6�   $��5,5,0,5q�6�   ��6X   �� $ MessageToSend�.��6�   5F      5N      5O      � 5P      $��1��6     5Q      �� 5R      �� 5S      .+�5U      �� $
��Left=�6   $��0��64   $��50,6,0,9q�6A   $��79��6U   $��35��6`   $��0q�6�   ��6l   Z� ��6�   �� *SendimageCommand+.��5V      �� $��..\Images\1814096-48.png��6   $��26��6>   5W      �� 5X      -   5         ��Name$ btnSendText��5X      $��1��6.   $
��Send��6;   $��
50,10,49,6q�6K   $��35��6`   $
��Bold"�6�   $��16(�6�   
�������$��0q�6�   $��79��6  ��6l   Z� ��6�   �� *SendMessageCommand+ �� UpdateSourceTrigger$ 	LostFocus=�6  5Z      5^      5`      5a      