﻿//vola se: StartCoroutine(Check());

private IEnumerator Check()
    {
        WWW w = new WWW("http://sentu.cz/api/datetime.php");
        yield return w;
        toRet = "NIC";
        if (w.error != null)
        {
            Debug.Log("Error .. " + w.error);
            // for example, often 'Error .. 404 Not Found'
        }
        else
        {
            Debug.Log("Found ... ==>" + w.text + "<==");
            toRet = w.text;
            // don't forget to look in the 'bottom section'
            // of Unity console to see the full text of
            // multiline console messages.
        }

        /* example code to separate all that text in to lines:
        longStringFromFile = w.text
        List<string> lines = new List<string>(
            longStringFromFile
            .Split(new string[] { "\r","\n" },
            StringSplitOptions.RemoveEmptyEntries) );
        // remove comment lines...
        lines = lines
            .Where(line => !(line.StartsWith("//")
                            || line.StartsWith("#")))
            .ToList();
        */
        //return toRet;

    }