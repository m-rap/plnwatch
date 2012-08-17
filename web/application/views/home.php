<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
    <head>
        <!--[if lt IE 7]>
        <div style='border: 1px solid #F7941D; background: #FEEFDA; text-align: center; clear: both; height: 75px; position: relative;'>
          <div style='position: absolute; right: 3px; top: 3px; font-family: courier new; font-weight: bold;'><a href='#' onclick='javascript:this.parentNode.parentNode.style.display="none"; return false;'><img src='http://www.ie6nomore.com/files/theme/ie6nomore-cornerx.jpg' style='border: none;' alt='Close this notice'/></a></div>
          <div style='width: 640px; margin: 0 auto; text-align: left; padding: 0; overflow: hidden; color: black;'>
            <div style='width: 75px; float: left;'><img src='http://www.ie6nomore.com/files/theme/ie6nomore-warning.jpg' alt='Warning!'/></div>
            <div style='width: 275px; float: left; font-family: Arial, sans-serif;'>
              <div style='font-size: 14px; font-weight: bold; margin-top: 12px;'>The browser you are currently using is not supported by www.pln.co.id</div>
              <div style='font-size: 12px; margin-top: 6px; line-height: 12px;'>For better experience please take the time to upgrade your browser.<br /></div>
            </div>
            <div style='width: 75px; float: left;'><a href='http://www.mozilla.com' target='_blank'><img src='http://www.ie6nomore.com/files/theme/ie6nomore-firefox.jpg' style='border: none;' alt='Get Firefox 3.5'/></a></div>
            <div style='width: 75px; float: left;'><a href='http://windows.microsoft.com/en-US/internet-explorer/downloads/ie' target='_blank'><img src='http://www.ie6nomore.com/files/theme/ie6nomore-ie8.jpg' style='border: none;' alt='Get Internet Explorer 8'/></a></div>
            <div style='width: 73px; float: left;'><a href='http://www.apple.com/safari/download/' target='_blank'><img src='http://www.ie6nomore.com/files/theme/ie6nomore-safari.jpg' style='border: none;' alt='Get Safari 4'/></a></div>
            <div style='float: left;'><a href='http://www.google.com/chrome/intl/id/landing.html#ext-kaskus' target='_blank'><img src='http://www.ie6nomore.com/files/theme/ie6nomore-chrome.jpg' style='border: none;' alt='Get Google Chrome'/></a></div>
          </div>
        </div>
        <![endif]-->
        <meta charset="UTF-8" />
        <meta http-equiv="Content-Type" content="text/html; charset=UTF-8"></meta>
        <title>PLN Watch | PT PLN (Persero) | Distribusi Jatim</title>
        <link rel="shortcut icon" href="<?php echo base_url(); ?>images/logo_pln.jpg" />
        <link rel="stylesheet" type="text/css" href="<?php echo base_url(); ?>css/pln_base.css" />
        <link rel="stylesheet" type="text/css" href="<?php echo base_url(); ?>css/pln_ext.css" />
        <script type="text/javascript" src="<?php echo base_url(); ?>js/jquery-1.7.2.min.js"></script>
        <script type="text/javascript" src="<?php echo base_url(); ?>js/jqueryslidemenu.js"></script>
        <script type="text/javascript">
            $(document).ready(function() {
                //Default Action
		$(".tab_content").hide(); //Hide all content
		$("ul.tabs li:first").addClass("active").show(); //Activate first tab
		$(".tab_content:first").show(); //Show first tab content
		//On Click Event
		$("ul.tabs li").click(function() {
                    $("ul.tabs li").removeClass("active"); //Remove any "active" class
                    $(this).addClass("active"); //Add "active" class to selected tab
                    $(".tab_content").hide(); //Hide all tab content
                    var activeTab = $(this).find("a").attr("href"); //Find the rel attribute value to identify the active tab + content
                    $(activeTab).fadeIn(); //Fade in the active content
                    return false;
                });
            });
        </script>
    </head>
    <body>
        <div align="center">
            <div id="awal" >
                <div id="header_pln">
                    <div id="header_pln_kiri" style="background-repeat:no-repeat;width:450px;float:left;">
                        <a  href="<?php echo site_url(); ?>"><img src="<?php echo base_url(); ?>images/pln-logo.jpg"  style="background-repeat:no-repeat;float:left;border:0px;"/></a>
                            <div>
                                <span style="margin-left:40px;">
                                    <h1 style="font:Tahoma, Geneva, sans-serif; font-size:18px;margin-left:0px;margin-top:35px;text-align:left;">PLN Watch</h1>
                                    <h3 style="font:Tahoma, Geneva, sans-serif; font-size:14px;margin-left:0px;text-align:left;">PT PLN (Persero) Distribusi Jawa Timur</h3>
                                </span>
                            </div>
                    </div>
                    <div id="header_pln_kanan" style="">
                        <div id="header_pln_kanan_1" style="float:right;">
                            <a  href="<?php echo site_url(); ?>"><img src="<?php echo base_url(); ?>images/ind.png" title="Indonesia" style="margin-top:12px;" align="left"/></a>
                            <a  href="<?php echo site_url(); ?>"><img src="<?php echo base_url(); ?>images/us.png" title="English" style="margin-top:12px; margin-left:5px; margin-right:3px;" align="left" /></a>
                            <div id="posisi_menu" style="padding-right:4px">
                                <ul>
                                    <li id="header_menu"><a href="<?php echo site_url(); ?>">CSR</a></li>
                                    <li id="header_menu">|</li>
                                    <li id="header_menu"><a href="<?php echo site_url(); ?>">Careers</a></li>
                                    <li id="header_menu">|</li>
                                    <li id="header_menu"><a href="<?php echo site_url(); ?>">Site Map</a></li>
                                    <li id="header_menu">|</li>
                                    <li id="header_menu"><a href="<?php echo site_url(); ?>">FAQ's</a></li>
                                    <li id="header_menu">|</li>
                                    <li id="header_menu"><a href="<?php echo site_url(); ?>">Kontak Kami</a></li></ul>
                            </div>
                        </div>
                        <div id="header_pln_kanan_2" align="right">
                            <img src="<?php echo base_url(); ?>images/header_pln_03_02.jpg" align="right"/><br />
                            <table>
                                <tr>
                                    <td>
                                        <input type="text" name="q" size="31" style="height:22px;"/>
                                    </td>
                                    <td>
                                        <input type="submit" name="sa" value="Cari" align="absbottom"  class="buttonCari"/>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </div>
                </div>
                <div id="menu_pln">
                    <div style="margin-left:14px;margin-top:0px;">
                        <div class="jqueryslidemenu_aw"></div>
                        <div id="myslidemenu" class="jqueryslidemenu">
                            <ul>
                                <li><a href="http://www.pln.co.id/disjatim"  class="first" >Beranda</a></li>
                                <li>
                                    <a href="#"  class="level1 " >Menu</a>
                                    <ul style="z-index: 1000;">
                                        <li style="z-index: 1000;"><a href="http://www.pln.co.id/disjatim/?p=108"><span style='margin-left:15px;'>Menu 1</span></a></li>
                                        <li style="z-index: 1000;"><a href="http://www.pln.co.id/disjatim/?p=42"><span style='margin-left:15px;'>Menu 2</span></a></li>
                                        <li style="z-index: 1000;"><a href="http://www.pln.co.id/disjatim/?p=42"><span style='margin-left:15px;'>Menu 3</span></a></li>
                                        <li style="z-index: 1000;"><a href="http://www.pln.co.id/disjatim/?p=42"><span style='margin-left:15px;'>Menu 4</span></a></li>
                                    </ul>
                                </li>
                            </ul>
                            <br style="clear: left" />
                        </div>
                        <div class="jqueryslidemenu_ak"></div>
                    </div>
		</div>
                <div id="content_pln">
a                    
                </div>
                <div id="footer_pln">
                    <div id="footer_pln_kiri">
                        <div id="footer_pln_kiri_1">
                            <div id="posisi_menu_bwh">
                                <ul>
                                    <li id="header_menu"><a href="http://www.pln.co.id/">Beranda</a></li>
                                    <li id="header_menu">|</li>
                                    <li id="header_menu"><a href="https://webmail.pln.co.id">Webmail</a></li>
                                    <li id="header_menu">|</li>
                                    <li id="header_menu"><a href="http://www.pln.co.id/?p=137">Produk &amp; Layanan</a></li>
                                    <li id="header_menu">|</li>
                                    <li id="header_menu"><a href="http://www.pln.co.id/?p=2260">Careers</a></li>
                                    <li id="header_menu">|</li>
                                    <li id="header_menu"><a href="http://www.pln.co.id/?p=129">CSR</a></li>
                                    <li id="header_menu">|</li>
                                    <li id="header_menu"><a href="http://www.pln.co.id/unit-pln">Unit PLN</a></li>
                                </ul>
                            </div>
                            <div id="footer_pln_kiri_2">Copyrights &copy; 2011 PT. PLN (Persero) All Rights Reserved</div>
                            <div id="footer_pln_kiri_3">
                                <a href="http://www.pln.co.id/?p=2069">Terms of Service</a>&nbsp;&nbsp;&nbsp;&nbsp;<a href="http://www.pln.co.id/?p=2071">Privacy Policy</a>
                            </div>                
			</div>
                    </div>
                    <div id="footer_pln_kanan_1">
                    <div id="footer_pln_kanan_2" style="padding:5px;">
                        <h3>
                            <span>
                                <h3>
                                    <span style="font:Tahoma, Geneva, sans-serif; font-size:14px;text-decoration:none">
                                        <a href="http://www.facebook.com/pages/Komunikasi-Korporat-PLN/169891086408522"><img src="/images/fb.png" width="17px" align="absmiddle" border="0" /></a>
                                        <a href="http://www.twitter.com/plnkita"><img src="/images/twit.png" width="17px" align="absmiddle" border="0" /></a>
                                        <a href="http://www.pln.co.id/?feed=rss2"><img src="/images/rss.png" width="17px" align="absmiddle" border="0" /></a>
                                        Ikuti Berita Dari Kami
                                    </span>
                                </h3>
                                <div style="font-family:tahoma; font-size:11px; font-style:normal" id='subs'>
                                    <span>
                                        <input type="text" name="nama" id="nama" size="100px" value="Nama Anda" onblur="if(this.value=='') this.value='Nama Anda';" onfocus="if(this.value=='Nama Anda') this.value='';" class="validate(required) input" style="margin-bottom:5px;width:255px;margin-top:5px;"/><br />
                                        <input type="text" name="email" id="email" size="100px" value="Alamat Email" onblur="if(this.value=='') this.value='Alamat Email';" onfocus="if(this.value=='Alamat Email') this.value='';" class="validate(email) input" style="width:255px;"/><br />
                                        <input type="button" class="buttonSubs" onclick="ajaxLoginFunction()" value="" style="margin-top:5px;"/>
                                    </span>
                                </div>
                            </span>
                        </h3>
                    </div>
                </div>
                </div>
            </div>
        </div>
    </body>
</html>