SELECT idpel, kodearea, jamnyala1, jamnyala2, jamnyala3, jamnyala2 - jamnyala1 as selisih FROM (
	SELECT d.idpel as idpel, d.kodearea as kodearea, a.jamnyala as jamnyala1, b.jamnyala as jamnyala2, c.jamnyala as jamnyala3
	FROM DIL d JOIN
		 sorek_062012 a ON d.idpel = a.idpel JOIN 
		 sorek_072012 b ON d.idpel = b.idpel JOIN 
		 sorek_082012 c ON d.idpel = c.idpel
) s WHERE (jamnyala2 - jamnyala1) / jamnyala2 > 0.25

14.8069

SELECT idpel, jamnyala1, jamnyala2, jamnyala3, selisih FROM (
	SELECT idpel, kodearea, jamnyala1, jamnyala2, jamnyala3, 0.25 * jamnyala1 as selisih FROM (
		SELECT d.idpel as idpel, d.kodearea as kodearea, a.jamnyala as jamnyala1, b.jamnyala as jamnyala2, c.jamnyala as jamnyala3
		FROM DIL d JOIN
			 sorek_062012 a ON d.idpel = a.idpel JOIN 
			 sorek_072012 b ON d.idpel = b.idpel JOIN 
			 sorek_082012 c ON d.idpel = c.idpel
	) s WHERE (jamnyala2 - jamnyala1) / jamnyala2 > 0.25
) s WHERE jamnyala3 - jamnyala2 > selisih;

14.7242 