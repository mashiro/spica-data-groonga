DLL = Spica.Data.Groonga.dll
EXE = sdg.exe
OPT = -unsafe -r:System.Xml.Linq.dll -r:System.ServiceModel.Web.dll -r:System.Runtime.Serialization.dll
SRC = Context.cs Api.cs Enum.cs

all:
	gmcs $(OPT) -out:$(EXE) Main.cs $(SRC)

library:
	gmcs $(OPT) -target:library -out:$(DLL) $(SRC)
