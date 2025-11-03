/************************************************************
 * Code formatted by SoftTree SQL Assistant © v11.3.277
 * Time: 2/17/2025 10:48:00 AM
 ************************************************************/

INSERT INTO [AppAutoNumber]
  (
    [SRAutoNumber],
    [EffectiveDate],
    [Prefik],
    [SeparatorAfterPrefik],
    [IsUsedDepartment],
    [SeparatorAfterDept],
    [IsUsedYear],
    [YearDigit],
    [SeparatorAfterYear],
    [IsUsedMonth],
    [IsMonthInRomawi],
    [SeparatorAfterMonth],
    [IsUsedDay],
    [SeparatorAfterDay],
    [NumberLength],
    [NumberGroupLength],
    [NumberGroupSeparator],
    [NumberFormat],
    [SeparatorAfterNumber],
    [IsUsedYearToDateOrder],
    [LastUpdateDateTime],
    [LastUpdateByUserID]
  )
VALUES
  (
    'AppointmentNoWebService',
    '2025-02-16 00:00:00.000',
    'API',
    '-',
    0,
    '',
    1,
    2,
    '',
    1,
    0,
    '',
    1,
    '-',
    4,
    0,
    '',
    '',
    '',
    1,
    GETDATE(),
    'sci'
  );
