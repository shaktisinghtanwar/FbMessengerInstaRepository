   M S B A M L   `   `   `  ����F  BFb_InstaWpf, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null3clr-namespace:Fb_InstaWpf.ViewFb_InstaWpf.View  5   	   c _System.Windows.Interactivity, Version=4.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35qPclr-namespace:System.Windows.Interactivity;assembly=System.Windows.InteractivitySystem.Windows.Interactivity 5	   	   5clr-namespace:Fb_InstaWpf.ModelFb_InstaWpf.Model  5
   	       Fb_InstaWpf.ChatMessenger   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   0viewsclr-namespace:Fb_InstaWpf.View     5   	   J FWpfAnimatedGif, Version=1.4.15.0, Culture=neutral, PublicKeyToken=null,gif"http://wpfanimatedgif.codeplex.com  5   
   XiPclr-namespace:System.Windows.Interactivity;assembly=System.Windows.Interactivity  5	   	   +localclr-namespace:Fb_InstaWpf.Model  5
   	   -   5          � Title$  Fb_Insta Comment Posting��5   	   $	��450��6+   $	��937��68   $
��1100��5   	   $	��400��6    � WindowStartupLocation$ CenterScreen=�5   	   $��2q�5   	   $��Center=�64   .��5      �  � RowDefinitions 5   
   �� 5      $
��Auto �6   6,   �� 5      6   .+�5   
   � -   5        $��80��5      ���#154189.+�5      �� $��Right=�6    �� Orientation$ 
Horizontal=�65   � 5       � ColumnDefinitions 5      �� 5      $
��105* �6'   67   �� 5      $	��223 �6'   �� 5      $
��124* �6'   67   �� 5      $
��131* �6'   67   �� 5      $	��250 �6'   �� 5      $
��127* �6'   67   �� 5      $
��128* �6'   67   .+�5      �� -   5         ��Name$ btnUserLogins��5      $��0��60   $
��Bold"�6@   $��Add UserLogin��6R   $��Center=�6j   $
��Left=�6�   $��3,3,0,5q�6�   $��Center=�6�   $	��100��6�   $��32��6�   ��6�   ��  �� Path$ btnUserLogins_Click��� 5      -   5        $��cmbUser��5      $��1��6+   
�������$��15(�6N   $��10,8,2,8q�6\    �� DisplayMemberPath$ InboxUserName��6�    �� SelectedValuePath$ UserId��6�   $��0q�6�   $��Center=�6  $��32��6*  ��6o   �� *LoginUsersList+	 �� SelectedItem	 6�   �� $ 	LoginUser���5      �� 5       
 �� EndPoint$	
 0,1&�6.    �� 
StartPoint$	 0,0&�6=   .��5!      �  � Color$ 	#FFF0F0F0��6+    � Offset$ 0Q�6=   � 5"      $ 	#FFE7E7EB��6+   $ 1Q�6=   5#      5&      �� 5'      -   5        $ NewLoginButton��5'      $��2��62   $
��Bold"�6B   $��Login��6T   $
��Left=�6d   $��23,5,0,5q�6�   $	��100��6�   $��31��6�   ��6�   �� *LoginCommand+.  (System.Windows.Interactivity.Interaction   Triggers 5(      /  )System.Windows.Interactivity.EventTrigger  5)         	EventName$ Click��6)   .  (System.Windows.Interactivity.TriggerBase   Actions. 5*      6  0System.Windows.Interactivity.InvokeCommandAction     Command 64   �� *NewLoginButtonLoaded+   CommandParameter 6]   ��  �� ElementName$ NewLoginButton��5+      5-      �� 5/      $��3��6   $��Right=�6(   $
��Bold"�6K   $��15(�6]   $��Center=�6k   $��	PageName:��6�   
�������$��8,0,0,0q�6�   �� 50      -   5        $��cmbUserPageName��50      
�������$��15(�6E   $��4��6S   $ 
FbPageName��6�   $ FbPageId��6�   $��0q�6�   $��Center=�6�   $��32��6�   ��6e   �� *
PageList+��51      �� 52      $	
 0,1&�6.   $	 0,0&�6=   .��53      � $ 	#FFF0F0F0��6+   $ 0Q�6=   � 54      $ 	#FFE7E7EB��6+   $ 1Q�6=   55       59        5:      $ SelectionChanged��6)   . 5;         64   �� *ShowAllLeftSideData+ 6\   �� $ cmbUserPageName��5=      5?      �� 5@      -   5        $ btnFatchAllPages��5@      $��5��64   $
��Bold"�6D   $��Get All Pages��6V   $
��Left=�6n   $��23,0,0,0q�6�   $	��100��6�   $��31��6�   ��6�   �� *FetchAllLoggedinUserPages+�� 5I      -   5         ��Name$ ImageProgressbar��5I      $��6��62   $��	13,4,12,4q�6�   "  WpfAnimatedGif.ImageBehavior   AnimatedSource 6B   �� *DisplayProgressBarPath+ 5J        5K      $ Loaded��6)   . 5L         64   �� *ImageProgressBarLoaded+ 6_   �� $ ImageProgressbar��5N      5P      5U      5X      � 5Z   
   $��1��6   #��� ��6    5[      �� 5\      $	��27* �6#   $	��340��6/   6@   �� 5]      $	��70* �6#   61   �� 5^      $��0* �6#   .+�5b      � #��� ��6   .+�5c      �� -	   5         ��Name$ tabCtrl��5c      $	��Top=�6/      BorderBrush#���   6�    � DataContext 6G   ��  �� SelectedIndex 6_   �� $ TabControlSelectedIndex� 5d        5e      $ Loaded��6)   . 5f         64   �� *TabCtrlLoaded+ 6V   �� $ tabCtrl��5h      .@�5j      �� -
   5         ��Name$ TabLeftItemMessenger��5j      ��5k      �� 5l      $ 
Horizontal=�6)   .+�5m   "   �� 
������ $��0��6n   ��6C   �� *FbMessengerListCommand+.��5n   &   �� $��20��6,   $.��(Images/if_Facebook_Messenger_1298720.png��67   5o   #   5p      .��5r      $   Fb_InstaWpf.View.SocialTabView   6.   �� $  MessengerUserListViewModel�5s      �� 5u      -   5        $ TabLeftFacebookItem��5u      $��
-2,-1,-2,0q�6c   ��5v      �� 5w      
������ $��1��6f   ��6?   �� *FbPageInboxCommand+.��5x   "   �� $��20��6(   $��Images/facebook.png��63   5y      .��5{         6/   �� $ FacebookUserListViewModel�5|      �� 5~      -   5        $ TabLeftItemInsta��5~      $��41��68   $��Bottom=�6D   $
��Left=�6_   $��53��6z   $��	-2,-2,0,2q�6�   ��5      �� 5�      
������ $��2��6f   $��24��6{   $��0q�6�   $��35��6�    J� RenderTransformOrigin$ 0.5,0.5&�6�   ��6@   �� *InstaInboxCommand+z�5�   "   V� 5�   &   .~�5�   *   �� �� 5�   *   �� 5�   *    �� Angle$ 0.461Q�6:   T� 5�   *   5�   '   .��5�   "   �� $��28��6(   $��Images/instapic.jpg��63   $��20��6P   5�      .��5�         6.   �� $  InstagramUserListViewModel�5�      5�      5�      � 5�      $��1��6   ���	#FFE2E2E6$��0,38,0,0q�6;   .+�5�      � .+�5�      ��  �� TabStripPlacement$	 Top=�6!   $
��auto��6:   $��0,-5,0,0q�6G   .@�5�      �� -   5        $ TabRightItemMessenger��5�   -   $��0��6"   .��5�      -   'Fb_InstaWpf.View.SocialUserMessagesView  $��-3,-5,-3,-3q�6;    6P   �� $  MessengerUserListViewModel�5�      �� 5�      -   5        $ TabRightItemFacebook��5�   ,   $��0��6"   .��5�        $��-3,-5,-3,-3q�6;    6P   �� $ FacebookUserListViewModel�5�      �� 5�      -   5        $ TabRightItemInsta��5�   ,   $��0��6"   .��5�        $��-3,-5,-3,-3q�6;    6P   �� $  InstagramUserListViewModel�5�      5�      5�      5�      5�      5�      5�      