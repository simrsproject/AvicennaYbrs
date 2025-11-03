/************************************************************
 * Code formatted by SoftTree SQL Assistant © v11.3.277
 * Time: 10/11/2023 10:24:29 AM
 ************************************************************/

CREATE PROCEDURE sp_ApptQueueingGetListQueuePrefix(@p_Date DATETIME, @p_Prefix VARCHAR(6))
AS
	--DECLARE @p_Date       DATETIME = '2023-09-14',
	--        @p_Prefix     VARCHAR(6) = 'AN'
	
	SELECT DISTINCT x.ServiceUnitID,
	       su.ServiceUnitName,
	       x.ParamedicID,
	       p.ParamedicName,
	       x.Prefix
	FROM   (
	           SELECT aq.ServiceUnitID,
	                  ap.ParamedicID,
	                  SUBSTRING(aq.FormattedNo, 1, CHARINDEX('-', aq.FormattedNo, 1) - 1) AS Prefix,
	                  aq.FormattedNo,
	                  aq.QueueingDate
	           FROM   AppointmentQueueing AS aq WITH (NOLOCK)
	                  INNER JOIN Appointment AS ap WITH (NOLOCK)
	                       ON  aq.AppointmentNo = ap.AppointmentNo
	           UNION
	           SELECT aq.ServiceUnitID,
	                  r.ParamedicID,
	                  SUBSTRING(aq.FormattedNo, 1, CHARINDEX('-', aq.FormattedNo, 1) - 1) AS Prefix,
	                  aq.FormattedNo,
	                  aq.QueueingDate
	           FROM   AppointmentQueueing AS aq WITH (NOLOCK)
	                  INNER JOIN Registration AS r WITH (NOLOCK)
	                       ON  r.AppointmentNo = aq.AppointmentNo
	       ) x
	       INNER JOIN ServiceUnit  AS su WITH (NOLOCK)
	            ON  su.ServiceUnitID = x.ServiceUnitID
	       INNER JOIN Paramedic    AS p WITH (NOLOCK)
	            ON  p.ParamedicID = x.ParamedicID
	WHERE  x.QueueingDate = @p_Date
	       AND x.Prefix = @p_Prefix