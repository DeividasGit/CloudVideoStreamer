import { useState, useEffect, useCallback } from "react";
import { mediaContentService } from "../services/media/mediaContentService";

export default function useMediaContent() {
    const [ mediaContent, setMediaContent ] = useState([]);
    const [ loading, setLoading ] = useState(true);

    const fetchMediaContent = useCallback(async () => {
        try {
          setLoading(true);
          const response = await mediaContentService.getAll();
          setMediaContent(response);
        } catch (error) {
          console.error("Error fetching media content:", error);
        } finally {
          setLoading(false);
        }
      }, []); // No dependencies → won't be recreated on re-renders
    
      useEffect(() => {
        fetchMediaContent(); // Now we just call the memoized function
      }, [fetchMediaContent]);

      return { mediaContent, loading, refresh: fetchMediaContent };
}