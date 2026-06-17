"""Validate that the SQLite schema accepts every SQL statement the app actually runs.
Creates a temp DB from schema.sql, seeds sample rows, then executes each real query."""
import sqlite3, os, sys, datetime

HERE = os.path.dirname(os.path.abspath(__file__))
schema = open(os.path.join(HERE, "schema.sql"), encoding="utf-8").read()

con = sqlite3.connect(":memory:")
con.executescript(schema)
ok, fail = 0, 0
def run(label, sql, params=()):
    global ok, fail
    try:
        cur = con.execute(sql, params)
        rows = cur.fetchall() if sql.strip().lower().startswith("select") else None
        n = (" -> %d rows" % len(rows)) if rows is not None else ""
        print(f"  OK   {label}{n}")
        ok += 1
    except Exception as e:
        print(f"  FAIL {label}: {e}\n        SQL: {sql}")
        fail += 1

now = datetime.datetime(2024, 1, 2, 3, 4, 5)
# --- seed one fully-linked var so the views return rows ---
run("seed vars", "INSERT INTO vars (varName,varPath,creatorName,packageName,version,metaDate,varDate,filesize,scenes,looks,clothing,hairstyle,plugins,assets,morphs,pose,skin) VALUES (?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?)",
    ("Creator.Pack.1","D:/vars/Creator.Pack.1.var","Creator","Pack",1,now,now,12_582_912.0,2,1,0,0,0,0,3,0,1))
run("seed installStatus", "INSERT INTO installStatus (varName,Installed,Disabled) VALUES (?,?,?)", ("Creator.Pack.1", 1, 0))
run("seed scenes", "INSERT INTO scenes (varName,atomType,isPreset,scenePath,previewPic) VALUES (?,?,?,?,?)",
    ("Creator.Pack.1","scenes",0,"Saves/scene/x.json","Saves/scene/x.jpg"))
run("seed dependencies", "INSERT INTO dependencies (varName,dependency) VALUES (?,?)", ("Creator.Pack.1","Other.Thing.2"))
run("seed savedepens", "INSERT INTO savedepens (savepath,modidate,dependency) VALUES (?,?,?)", ("Saves/x.json", now, "Other.Thing.2"))

print("-- app SELECT commands --")
run("vars.Fill", "SELECT varName, varPath, creatorName, packageName, version, metaDate, varDate, filesize, scenes, looks, clothing, hairstyle, plugins, assets, morphs, pose, skin FROM vars")
run("scenes.Fill", "SELECT ID, varName, atomType, isPreset, scenePath, previewPic FROM scenes")
run("scenes.FillBy(varName)", "SELECT ID, varName, atomType, isPreset, scenePath, previewPic FROM scenes WHERE (varName = ?)", ("Creator.Pack.1",))
run("scenes.FillByLoadable", "SELECT ID, varName, atomType, isPreset, scenePath, previewPic FROM scenes WHERE (atomType = 'scenes') OR (isPreset = true)")
run("installStatus.Fill", "SELECT varName, Installed, Disabled FROM installStatus")
run("dependencies.Fill", "SELECT ID, varName, dependency FROM dependencies")
run("savedepens.Fill", "SELECT ID, savepath, modidate, dependency FROM savedepens")
run("savedepens.FillByDepens", "SELECT ID, savepath, modidate, dependency FROM savedepens whe WHERE (dependency = ?)", ("Other.Thing.2",))
run("installedScenes.Fill (VIEW)", "SELECT ID, atomType, varName, scenePath, previewPic, metaDate, varDate, Installed FROM installedScenes")
run("varsView.Fill (VIEW)", "SELECT varName, varPath, creatorName, packageName, version, metaDate, varDate, filesize, scenes, looks, clothing, hairstyle, plugins, assets, morphs, pose, skin, Installed, Disabled, fsize FROM varsView")

print("-- app CRUD commands (optimistic-concurrency WHERE, positional ?) --")
run("dependencies.Insert", "INSERT INTO `dependencies` (`varName`, `dependency`) VALUES (?, ?)", ("a","b"))
run("dependencies.Update", "UPDATE `dependencies` SET `varName` = ?, `dependency` = ? WHERE ((`ID` = ?) AND ((? = 1 AND `varName` IS NULL) OR (`varName` = ?)) AND ((? = 1 AND `dependency` IS NULL) OR (`dependency` = ?)))", ("a2","b2",1,0,"Creator.Pack.1",0,"Other.Thing.2"))
run("scenes.Update", "UPDATE `scenes` SET `varName` = ?, `atomType` = ?, `isPreset` = ?, `scenePath` = ?, `previewPic` = ? WHERE ((`ID` = ?) AND ((? = 1 AND `varName` IS NULL) OR (`varName` = ?)) AND ((? = 1 AND `atomType` IS NULL) OR (`atomType` = ?)) AND ((? = 1 AND `isPreset` IS NULL) OR (`isPreset` = ?)) AND ((? = 1 AND `scenePath` IS NULL) OR (`scenePath` = ?)) AND ((? = 1 AND `previewPic` IS NULL) OR (`previewPic` = ?)))", ("Creator.Pack.1","scenes",0,"p","q",1,0,"Creator.Pack.1",0,"scenes",0,0,0,"Saves/scene/x.json",0,"Saves/scene/x.jpg"))
run("installStatus.DeleteAll (was 'DELETE *')", "DELETE FROM `installStatus`")

print(f"\nRESULT: {ok} OK, {fail} FAIL")
sys.exit(1 if fail else 0)
