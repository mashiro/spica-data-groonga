DLL = Spica.Data.Groonga.dll
EXE = Spica.Data.Groonga.exe
OPT = -unsafe -r:System.Xml.Linq.dll -r:System.ServiceModel.Web.dll -r:System.Runtime.Serialization.dll
SRC = Context.cs Api.cs Enum.cs

all:
	gmcs $(OPT) -target:library -out:$(DLL) $(SRC)

interpreter:
	gmcs $(OPT) -out:$(EXE) Interpreter.cs $(SRC)

