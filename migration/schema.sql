-- ============================================================================
-- varManager SQLite schema  (faithful port of the original Access/Jet varManager.mdb)
--
-- Generated for the .NET 8 + SQLite modernization of varManager.
-- Column types/keys/indexes mirror the original Access database (extracted via ADOX).
-- The two Access saved queries (installedScenes, varsView) are recreated as SQLite VIEWs.
-- scenesView / HideFav are NOT here: they are in-memory DataTables in the app, never DB-backed.
--
-- Booleans are stored as 0/1 (SQLite has no bool; declared type BOOLEAN lets the
-- ADO.NET provider surface them as System.Boolean for the typed DataSet).
-- Date/time columns use declared type DATETIME (provider stores ISO-8601 text).
-- ============================================================================

PRAGMA foreign_keys = OFF;

-- ---- vars -------------------------------------------------------------------
CREATE TABLE IF NOT EXISTS vars (
    varName     TEXT    NOT NULL PRIMARY KEY,
    varPath     TEXT,
    creatorName TEXT,
    packageName TEXT,
    version     INTEGER,
    metaDate    DATETIME,
    varDate     DATETIME,
    filesize    REAL,
    scenes      INTEGER,
    looks       INTEGER,
    clothing    INTEGER,
    hairstyle   INTEGER,
    plugins     INTEGER,
    assets      INTEGER,
    morphs      INTEGER,
    pose        INTEGER,
    skin        INTEGER
);
CREATE INDEX IF NOT EXISTS idx_vars_creatorName ON vars(creatorName);
CREATE INDEX IF NOT EXISTS idx_vars_packageName ON vars(packageName);
CREATE INDEX IF NOT EXISTS idx_vars_varDate     ON vars(varDate);
CREATE INDEX IF NOT EXISTS idx_vars_scenes      ON vars(scenes);
CREATE INDEX IF NOT EXISTS idx_vars_looks       ON vars(looks);
CREATE INDEX IF NOT EXISTS idx_vars_clothing    ON vars(clothing);
CREATE INDEX IF NOT EXISTS idx_vars_hairstyle   ON vars(hairstyle);
CREATE INDEX IF NOT EXISTS idx_vars_plugins     ON vars(plugins);
CREATE INDEX IF NOT EXISTS idx_vars_assets      ON vars(assets);

-- ---- scenes -----------------------------------------------------------------
CREATE TABLE IF NOT EXISTS scenes (
    ID          INTEGER PRIMARY KEY AUTOINCREMENT,
    varName     TEXT,
    atomType    TEXT,
    isPreset    BOOLEAN,
    scenePath   TEXT,
    previewPic  TEXT
);
CREATE INDEX IF NOT EXISTS idx_scenes_atomType ON scenes(atomType);
CREATE INDEX IF NOT EXISTS idx_scenes_varName  ON scenes(varName);

-- ---- installStatus ----------------------------------------------------------
CREATE TABLE IF NOT EXISTS installStatus (
    varName     TEXT    NOT NULL PRIMARY KEY,
    Installed   BOOLEAN,
    Disabled    BOOLEAN
);

-- ---- dependencies -----------------------------------------------------------
CREATE TABLE IF NOT EXISTS dependencies (
    ID          INTEGER PRIMARY KEY AUTOINCREMENT,
    varName     TEXT    NOT NULL,
    dependency  TEXT    NOT NULL
);
CREATE INDEX IF NOT EXISTS idx_dependencies_dependency ON dependencies(dependency);
CREATE INDEX IF NOT EXISTS idx_dependencies_varName    ON dependencies(varName);

-- ---- savedepens -------------------------------------------------------------
CREATE TABLE IF NOT EXISTS savedepens (
    ID          INTEGER PRIMARY KEY AUTOINCREMENT,
    savepath    TEXT    NOT NULL,
    modidate    DATETIME,
    dependency  TEXT    NOT NULL
);
CREATE INDEX IF NOT EXISTS idx_savedepens_dependency ON savedepens(dependency);
CREATE INDEX IF NOT EXISTS idx_savedepens_savepath   ON savedepens(savepath);

-- ---- VIEW: installedScenes  (Access INNER JOINs; Installed=True -> =1) -------
CREATE VIEW IF NOT EXISTS installedScenes AS
SELECT scenes.*, vars.metaDate, vars.varDate, installStatus.Installed
FROM (installStatus INNER JOIN scenes ON installStatus.varName = scenes.varName)
     INNER JOIN vars ON scenes.varName = vars.varName
WHERE installStatus.Installed = 1;

-- ---- VIEW: varsView  (Access LEFT JOIN; [filesize]/1024/1024 AS fsize) -------
CREATE VIEW IF NOT EXISTS varsView AS
SELECT vars.*, installStatus.Installed, installStatus.Disabled, filesize / 1024 / 1024 AS fsize
FROM vars LEFT JOIN installStatus ON vars.varName = installStatus.varName;
