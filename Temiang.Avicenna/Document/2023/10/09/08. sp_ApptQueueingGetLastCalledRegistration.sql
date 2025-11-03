/************************************************************
 * Code formatted by SoftTree SQL Assistant © v11.3.277
 * Time: 10/9/2023 8:01:36 PM
 ************************************************************/

CREATE PROCEDURE sp_ApptQueueingGetLastCalledRegistration(@p_Date DATETIME)
AS
	--DECLARE @p_Date DATETIME = '2023-10-05'
	
	;WITH LastQueue AS (
	    SELECT aq.Id,
	           aq.SRQueueingLocation,
	           aq.SRQueueingGroup,
	           aq.SRQueueingType,
	           aq.ProcessDateTime,
	           aq.ServiceUnitID,
	           aq.FormattedNo,
	           aq.CounterCode,
	           ROW_NUMBER() OVER(
	               PARTITION BY SRQueueingLocation, CounterCode ORDER BY ProcessDateTime DESC
	           )                    AS rn
	    FROM   AppointmentQueueing  AS aq WITH (NOLOCK)
	    WHERE  aq.QueueingDate = @p_Date
	           AND aq.SRQueueingGroup = '01'
	           AND aq.SRKioskQueueStatus IN ('02', '03')
	           AND aq.ProcessDateTime IS NOT NULL
	)
	
	SELECT lq.SRQueueingGroup,
	       a2.ItemName             AS 'QueueingGroup',
	       lq.SRQueueingLocation,
	       a1.ItemName             AS 'QueueingLocation',
	       lq.SRQueueingType,
	       a3.ItemName             AS 'QueueingType',
	       lq.FormattedNo          AS 'QueueCode',
	       lq.CounterCode          AS 'Counter',
	       lq.ProcessDateTime,
	       lq.ServiceUnitID,
	       su.ServiceUnitName
	FROM   LastQueue               AS lq WITH (NOLOCK)
	       INNER JOIN AppStandardReferenceItem AS a1 WITH (NOLOCK)
	            ON  a1.StandardReferenceID = 'QueueingLocation'
	            AND a1.ItemID = lq.SRQueueingLocation
	       INNER JOIN AppStandardReferenceItem AS a2 WITH (NOLOCK)
	            ON  a2.StandardReferenceID = 'QueueingGroup'
	            AND a2.ItemID = lq.SRQueueingGroup
	       INNER JOIN AppStandardReferenceItem AS a3 WITH (NOLOCK)
	            ON  a3.StandardReferenceID = 'QueueingType'
	            AND a3.ItemID = lq.SRQueueingType
	       INNER JOIN ServiceUnit  AS su WITH (NOLOCK)
	            ON  su.ServiceUnitID = lq.ServiceUnitID
	WHERE  rn = 1
	