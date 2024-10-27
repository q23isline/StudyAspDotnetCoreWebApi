IF NOT EXISTS(
    SELECT
        *
    FROM
        sys.databases
    WHERE
        name = 'StudyAspDotnetCoreWebApi'
) BEGIN CREATE DATABASE StudyAspDotnetCoreWebApi;

END
GO
