DLL = Spica.Data.Groonga.dll
EXE = sdg.exe
OPT = -r:System.Xml.Linq.dll -r:System.ServiceModel.Web.dll -r:System.Runtime.Serialization.dll
SRC = Context.cs Api.cs Enum.cs

all:
	gmcs -unsafe -out:$(EXE) $(OPT) Main.cs $(SRC)

library:
	gmcs -unsafe -target:library -out:$(DLL) $(OPT) $(SRC)
