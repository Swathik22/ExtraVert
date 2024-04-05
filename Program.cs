// See https=//aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");

List<Plant> plants=new List<Plant>()
{
    new Plant()
    {
        Species= "Cucumber",
        LightNeeds= 1,
        AskingPrice= 27.00M,
        City= "BlueAsh",
        ZIP= 482044,
        Sold= true
    }, 
    new Plant()
    {
        Species= "Okra",
        LightNeeds= 2,
        AskingPrice= 12.99M,
        City= "Mount Juliet",
        ZIP= 242899,
        Sold= false
    }, 
    new Plant()
    {
        Species= "Garlic",
        LightNeeds= 3,
        AskingPrice= 71.67M,
        City= "Nashville",
        ZIP= 111222,
        Sold= false
    }, 
    new Plant()
    {
        Species= "Rose",
        LightNeeds= 4,
        AskingPrice= 50.88M,
        City= "Brentwood",
        ZIP= 333555,
        Sold= true
    }, 
    new Plant()
    {
        Species= "Lilly",
        LightNeeds= 5,
        AskingPrice= 54.22M,
        City= "Franklin",
        ZIP= 222555,
        Sold= true
    }
};

void generateRandomPlant()
{
    Random random=new Random();
    int i=random.Next(0,plants.Count);
    Plant availablePlant=null;
  
    while(availablePlant==null)
    {
        i=random.Next(0,plants.Count);
        if(!plants[i].Sold)
        {
            availablePlant=plants[i];
        }
    }
    Console.WriteLine($"{availablePlant.Species} in {availablePlant.City} {(availablePlant.Sold?"was Sold":"is available")} for {availablePlant.AskingPrice} dollars");
    // Console.WriteLine($"{plants[i].Species} in {plants[i].City} {(plants[i].Sold?"was Sold":"is available")} for {plants[i].AskingPrice} dollars");

}

//Display plants based on light needs
void plantsByLightNeeds()
{
    Console.WriteLine("Enter the maximum LightNeeds between 1 and 5");
    int lightRequired=int.Parse(Console.ReadLine());

    List<Plant> plantsForMaxLightRequired=new List<Plant>();

    foreach(Plant eachPlant in plants)
    {
        if(eachPlant.LightNeeds<=lightRequired)
        {
            plantsForMaxLightRequired.Add(eachPlant);
        }
    }

    if(plantsForMaxLightRequired.Count>0)
    {
        for(int i=0;i<plantsForMaxLightRequired.Count;i++)
        {
            Console.WriteLine($"{i+1}. {plantsForMaxLightRequired[i].Species} in {plantsForMaxLightRequired[i].City} {(plantsForMaxLightRequired[i].Sold?"was Sold":"is available")} for {plantsForMaxLightRequired[i].AskingPrice} dollars");
        }   
    }

}


//Display Lowest Price plants
void budgetFriendlyPlants()
{
    Plant lowestPricePlant=new Plant();

    List<Plant> sortingPlantsInAsc=new List<Plant>();

    for(int i=0;i<plants.Count-1;i++)
    {
        for(int j=0;j<plants.Count-i-1;j++)
        {
            if(plants[j].AskingPrice > plants[j+1].AskingPrice)
            {
                Plant tempPlant=plants[j];
                plants[j]=plants[j+1];
                plants[j+1]=tempPlant;
            }
        }
        
    }

    lowestPricePlant = plants.FirstOrDefault();

    if(lowestPricePlant!=null)
    {
        Console.WriteLine("The budget friendly Plant is :");
        Console.WriteLine($"{lowestPricePlant.Species}");
    }  
}

//display Sold and available plants count
void countOfSoldAndAvailablePlants()
{
    int soldCount=0;
    int availableCount=0;

    for(int i=0;i<plants.Count;i++)
    {
        //plants[i].Sold?soldCount++:availableCount++;
        if(plants[i].Sold)
        {
            soldCount++;
        }
        else
        {
            availableCount++;
        }
    }

    
    Console.WriteLine(@$"
    Sold No. of Plants: {soldCount}
    Available No. of plants: {availableCount}");
}

void averageLightNeeds()
{
    int averageLightNeeds=0;
    int sum=0;
    for(int i=0;i<plants.Count;i++)
    {
        sum=sum+plants[i].LightNeeds;
    }
    averageLightNeeds=sum/plants.Count;
    Console.WriteLine($"The average light Needs for plants ${averageLightNeeds}");
}
//Displaying all Plants names
void displayAllPlants()
{
    Console.WriteLine("Plants:");
    for(int i=0;i<plants.Count;i++)
    {
        Console.WriteLine($"{i+1}. {plantDetails(plants[i])} in {plants[i].City} {(plants[i].Sold?"was Sold":"is available")} for {plants[i].AskingPrice} dollars");
    }
}

void percentageOfPlantsAdopted()
{
     double adoptedAverage=0.00;
    double sum=0.00;
    for(int i=0;i<plants.Count;i++)
    {
        if(plants[i].Sold)
        {
            sum++;
        }
    }
    adoptedAverage=sum/plants.Count;
    Console.WriteLine($"The percentage of plants adopted is ${adoptedAverage}");
}
//Post A Plant
void postAPlant()
{
    Plant newPlant=new Plant();

    Console.WriteLine("Enter the plant details to be Posted to Adopt");

    Console.WriteLine("Enter the Plant Species name");
    newPlant.Species=Console.ReadLine();

    Console.WriteLine("Enter the Plant LightNeeds on a scale of 1 to 5");
    newPlant.LightNeeds=int.Parse(Console.ReadLine());

    Console.WriteLine("Enter the plant asking price");
    newPlant.AskingPrice=Convert.ToDecimal(Console.ReadLine());

    Console.WriteLine("Enter the plant city");
    newPlant.City=Console.ReadLine();

    Console.WriteLine("Enter the ZIP code");
    newPlant.ZIP=int.Parse(Console.ReadLine());

    Console.WriteLine("Enter Expiry date for this post");
    try
    {
    Console.Write("Year");
    int year=int.Parse(Console.ReadLine());
    Console.Write("Month");
    int month=int.Parse(Console.ReadLine()); 
    Console.Write("Day");
    int day=int.Parse(Console.ReadLine());
    
    newPlant.AvailableUntil=new DateTime(year,month,day);

    newPlant.Sold=false;

    plants.Add(newPlant);
    }
    catch(ArgumentOutOfRangeException)
    {
        Console.WriteLine("Invalid date : Enter date in the format (year,month,day)");
    }
}

//Adopt a Plant
void adoptAPlant()
{
    List<Plant> availablePlants=new List<Plant>();
    foreach(Plant eachPlant in plants)
    {    
        if(!eachPlant.Sold&&DateTime.Now<=eachPlant.AvailableUntil)
        {
            availablePlants.Add(eachPlant);
        }
        
    }
    if(availablePlants.Count>0)
    {
        Console.WriteLine(@"Available Plants:
        Choose The plant you want to Adopt");
        
        for(int i=0;i<availablePlants.Count;i++)
        {
            Console.WriteLine($"{i+1}. {availablePlants[i].Species} in {availablePlants[i].City} {(availablePlants[i].Sold?"was Sold":"is available")} for {availablePlants[i].AskingPrice} dollars");
        }
    }
    int optionChoosen=0;
    try
    {
        optionChoosen=int.Parse(Console.ReadLine());
        if(optionChoosen>0||optionChoosen<availablePlants.Count)    
        {
            availablePlants[optionChoosen-1].Sold=true;
        }
        if(availablePlants[optionChoosen-1].Sold)
        {
            Console.WriteLine($"Congragulations! The plant you choosen is {availablePlants[optionChoosen-1].Species}");
        }
    }
    catch(FormatException)
    {
        Console.WriteLine("Wrong option Choosen.");
    }
    catch(Exception ex)
    {
        Console.WriteLine("Do Better");
    }

        
}

void delistAPlant()
{
    displayAllPlants();
    Console.WriteLine("Choose an option to Delist a plant");
    int optionToDelist=int.Parse(Console.ReadLine());

    int plantsBeforeCount=plants.Count;
    plants.RemoveAt(optionToDelist-1);
    int plantsAfterCount=plants.Count;

    if(plantsAfterCount<plantsBeforeCount)
    {
        Console.WriteLine("Plant removed successfully!.");
    }

}

string plantDetails(Plant plant)
{
    string plantName=plant.Species;
    return plantName;
}


//Displaying the user to Choose an option
Console.Clear();
Console.ReadKey();
string greetings="Save Soil by Planting";
Console.WriteLine(greetings);
string choice=null;
while(choice!="a")
{
    
    Console.WriteLine(@"Choose an Option:        
                    a. Exit
                    b. Display all Plants
                    c. Post a plant to be adopted
                    d. Adopt a Plant
                    e. Delist a Plant
                    f. Display a Random Plant
                    g. Display plants based on LightNeeds
                    h. Lowest Price Plants
                    i. Number of Plants Available(Not Sold, Still Available
                    j. Average LightNeeds
                    k. Percentage of Plants adopted
                    ");
    
    
    choice=Console.ReadLine();
    
   switch(choice)
    {
        case "b": 
                displayAllPlants();
                break;
        case "c":
            postAPlant();
            break;
            
        case "d":
            adoptAPlant();
            break;
       
        case "e":
            delistAPlant();
            break;

        case "f":
            generateRandomPlant();
            break;

        case "g":
            plantsByLightNeeds();
            break;

        case "h":
            budgetFriendlyPlants();
            break;

        case "i":
            countOfSoldAndAvailablePlants();
            break;

        case "j":
            averageLightNeeds();
            break;

        case "k":
            percentageOfPlantsAdopted();
            break;

        default:
            Console.WriteLine("GoodBye");
            break;
    }
    
}