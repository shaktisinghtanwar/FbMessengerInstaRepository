   M S B A M L   `   `   `  ����F  BFb_InstaWpf, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null7 clr-namespace:Fb_InstaWpf.HelperFb_InstaWpf.Helper  5   	   c _System.Windows.Interactivity, Version=4.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35qPclr-namespace:System.Windows.Interactivity;assembly=System.Windows.InteractivitySystem.Windows.Interactivity 5          Fb_InstaWpf.AddLoginUsers   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   3helper clr-namespace:Fb_InstaWpf.Helper     5   	   ^iPclr-namespace:System.Windows.Interactivity;assembly=System.Windows.Interactivity     5        � Title$  AddLoginUsers��5   	   $	��317��6   $	��317��6,   $	��317��6<   $	��430��6L   $	��430��6\   $��430.184��6k    � WindowStartupLocation$ CenterScreen=�6{    � WindowStyle$ ThreeDBorderWindow=�6�   .��5      � .+�5   
   � ���#794044 � RowDefinitions 5	      �� 5
      $	��47* �6    6/   �� 5      $	��69* �6    6/   �� 5      $	��25* �6    6/   .+�5      � $��0��6   ���#794044 5      �� 5      6&   �� 5      6&    � ColumnDefinitions 5      �� 5      $	��30* �6'   �� 5      $	��73* �6'   .+�5      �� -   5        $��Lblproxy��5      $
��Left=�6E   $��	-2,-3,0,0q�6`   $	��Top=�6s   $��5��6�   $��5��6�   ��6(   �� *
Lblproxy+6�   �� 5      $��User Id��6   $��0��6*   $
��Bold"�67   
�������$��0��6\   $��Center=�6m   $��Center=�6�   $��
9,12,52,12q�6�   $��24��6�   $��62��6�   �� 5      -   5        $��TxtUsername��5       �� 	MaxLength$ 50��6-   $
��Left=�6<   $��0��6W   $��1��6d   $��24��6t   $��Center=�6�   $	��270��6�   $��
12,12,0,12q�6�   ��6�   �� *	TxtUserId+ �� Mode$ TwoWay=�6�   �� 5      $��Password��6   $
��Bold"�6,   $��Center=�6>   
�������$��1��6n   $��0��6{   $��Center=�6�   $��26��6�   $��11,11,40,10q�6�   $��72��6�   �� 5      -   5        $��TxtPassword��5      $ 50��6-   $
��Left=�6<   $��23��6W   $��1��6c   $��1��6p    �� TextWrapping$
 Wrap=�6�   $��Center=�6�   $	��270��6�   $��
12,12,0,12q�6�   ��6�   �� *TxtPassword+$ TwoWay=�6�   5      � 5      $��1��6   
���ؿ��$��0,0,0,47q�65   $��2��6G   .+�5      ]� ���#794044.+�5      �� -   5        $��ListViewLoginUserId��5      $��0q�6�   
�������$	��387��6�    �� SelectedItem 6:   �� 	 �� Path$	 LoginUserName���6g   �� *NewUserNameInfoList+
 �� View
 5!      �� 5"      .P�5#   "   �� $��UserName��61    �� DisplayMemberBinding 6C   �� *LoginUserName+5$      5(      5)      5*      � 5+      $��1��6   $��Bottom=�6"   $��53��6=   $��2��6I   ���#794044 5,      �� 5-      �� 5.      �� 5/      .+�51      �� -   5        $��btnSave��51      $
��Bold"�6)   $��1��6;   $
��Save��6K   $��Center=�6~   $��Center=�6�   $��75��6�   ��6[   �� *NewUserCommand+6�   �� 52      -   5        $��btnNew��52      $
��Bold"�6'   $��0��69   $	��New��6I   $��Center=�6X   $��Center=�6v   $��75��6�   ��6�   �� *NewUserClear+6�   �� 53      -   5        $��	btnDelete��53      $
��Bold"�6*   $��2��6<   $��Delete��6L   $��Center=�6]   $��Center=�6{   $��75��6�   ��6�   �� *clearListViewItemCommand+6�   54      55      57      58      