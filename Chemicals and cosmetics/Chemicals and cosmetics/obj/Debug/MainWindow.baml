   M S B A M L   `   `   `  ����R  NChemicals and cosmetics, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullA%clr-namespace:Chemicals_and_cosmeticsChemicals_and_cosmetics  5   	   (    "Chemicals_and_cosmetics.MainWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   7local%clr-namespace:Chemicals_and_cosmetics     5   	     � Title$  
MainWindow��5   	   $	��450��6   $	��800��6)   .��5	      �  � RowDefinitions 5
   
   �� 5      $��* �6   �� 5      $��* �6    � ColumnDefinitions 5   
   �� 5      $��* �6   �� 5      $��* �6   .+�5   
   �� $��Chemicals in Cosmetics��6   $��0��61   $��0��6>   $��Arial Black)�6N   $��50(�6g   $��Center=�6u   $��2��6�   $��Center=�6�   �� 5   
   -   5         ��Name$ companyPageBtn��5      $��1��6)   $
��Bold"�66   $��0��6H   $	��150��6X   $��50��6d   $��Company��6p   $��25(�6�   ���	#FF120808���	#FFF1BBBB � 	Resources 5       �� 
TargetType �� Property �� Value��%^   '��       �� 5      " ��6   .�5      ��  �@��CornerRadius$ 5��65   5      5      5      �� 5   
   -   5        $ clientPageBtn��5      $
��Bold"�6(   $��1��6:   $��1��6G   $	��150��6W   $��50��6c   $��Client��6o   $��25(�6�   
���   ����	#FF120808���	#FFF1BBBB 5      ��%^   '��       �� 5      " ��6   .�5      ��  �@��CornerRadius$ 5��65   5       5!      5"      ��5#   
   �� 5$       �� ImageSource$ background-homeScreen.png��6   	 �� Opacity$		 0.6Q�6A   5'      5(      