   M S B A M L   `   `   `  ����g  cMicrosoft.ReportViewer.WinForms, Version=12.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91tSclr-namespace:Microsoft.Reporting.WinForms;assembly=Microsoft.ReportViewer.WinFormsMicrosoft.Reporting.WinForms  5   	    WpfApplication1    WpfApplication1.MainWindow   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   X TXceed.Wpf.Toolkit, Version=3.2.0.0, Culture=neutral, PublicKeyToken=3e4669d2f30244f44xctk)http://schemas.xceed.com/wpf/xaml/toolkit  5   	   \rvSclr-namespace:Microsoft.Reporting.WinForms;assembly=Microsoft.ReportViewer.WinForms  5   	     � Title$  
MainWindow��5   	   $��559.947��6   $	��806��6-   .��5      �  � ColumnDefinitions 5   
   �� 5	      $
��637* �6   �� 5
      $
��163* �6   .+�5   
   �� -   5        $��	btnYazdir��5      $	��100��6"   $��25��6.   $��Right=�6:   $	��Top=�6V   $��	0,40,10,0q�6n   $��Yazdır��6�   $��1��6�   6�   �� 5   
   -   5         ��Name$ 
scan_twain��5      $	��100��5      $��25��6   $��Right=�5      $	��Top=�6-   $��	0,10,10,0q�6E   $
��Tara��5      $��1��5      �� 5   
   -   5         ��Name$ image1��5      $
��Left=�5      $	��Top=�6+   $��	26,27,0,0q�6C   $	��232��5      $	��424��6   $��0��5      �� 5   
   -   5         ��Name$ lblMusteriBilgiFormu��5      $��MÜŞTERİ BİLGİ FORMU��5      $
��Left=�5      $	��Top=�6+   $��218,281,0,0q�6C   $
��Bold"�6X   �� 5   
   -   5        $ image2��5      $
��Left=�5      $	��Top=�6(   $��
350,27,0,0q�6@   $	��232��5      $	��256��6   $��0��5      �� 5   
   -   5        $ label��5      $��Tarih:��6   $��
64,312,0,0q�60   $	��Top=�6D   $��11(�6\   $
��Bold"�6j   $
��Left=�6|   �� 5    
   -   5        $ 
label_Copy��5       $��Fiş Belge No:��6$   $��
26,343,0,0q�6<   $	��Top=�6P   $��11(�6h   $
��Bold"�6v   $
��Left=�6�   �� 5!   
   -   5        $ label_Copy1��5!      $��
Kayıt No:��6%   $��
45,374,0,0q�69   $	��Top=�6M   $��11(�6e   $
��Bold"�6s   $
��Left=�6�   �� 5"   
   -	   5        $ label_Copy2��5"      $��Adres:��6%   $��
59,436,0,0q�66   $	��Top=�6J   $��11(�6b   $
��Bold"�6p   $
��Left=�6�   �� 5#   
   -
   5        $ label_Copy3��5#      $��Telefon:��6%   $��
52,405,0,0q�68   $	��Top=�6L   $��11(�6d   $
��Bold"�6r   $
��Left=�6�   �� 5$   
   -   5         ��Name$ txtTarih��5$      $
��Left=�6$   $��23��6?   $��108,312,0,0q�6K    �� TextWrapping$
 Wrap=�6`   $	��Top=�6t   $	��195��6�   �� 5%   
   -   5        $ txtFisNo��5%      $
��Left=�6$   $��23��6?   $��108,343,0,0q�6K   $
 Wrap=�6`   $	��Top=�6t   $	��195��6�   �� 5&   
   -   5        $ 	txKayitNo��5&      $
��Left=�6%   $��23��6@   $��108,374,0,0q�6L   $
 Wrap=�6a   $	��Top=�6u   $	��195��6�   %  Xceed.Wpf.Toolkit.MaskedTextBox  5'   
   -   5          Name$ 	txTelefon��5'         Mask$ 000-000-00-00��60   	 �� 
CaretIndex$	 0��6E   $
��Left=�6T   $��23��6o   $��108,405,0,0q�6{   $	��Top=�6�   $	��195��6�   $��12(�6�   �� 5(   
   -   5        $ tbAdres��5(      $
��Left=�6#   $��69��6>   $��108,436,0,0q�6J   $
 Wrap=�6_   
 �� AcceptsReturn
 .  �� 
AcceptsTab . $	��Top=�6�   $	��195��6�   7  1System.Windows.Forms.Integration.WindowsFormsHost  5)   
   $
��Left=�6   $	��262��66   $��358,135,0,0q�6C   $	��Top=�6X   $	��389��6p   $��2��6|      Child. 5*      /   )Microsoft.Reporting.WinForms.ReportViewer  -   5        reportViewer��5-      5.      5/      50      