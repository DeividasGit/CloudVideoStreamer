import { useState, useEffect } from "react";
import { mediaContentService } from "../../services/media/mediaContentService";
import MediaContent from "../../models/MediaContent";

export default function Home() {
  const [ mediaContent, setMediaContent ] = useState([]);
  const [ loading, setLoading ] = useState(true);

  useEffect(() => {
    const fetchMediaContent = async () => {
      try {
        setLoading(true);
        const response = await mediaContentService.getAll();
        setMediaContent(response);
      } catch (error) {
        console.error("Error fetching media content:", error);
      } finally {
        setLoading(false);
      }
    };
    fetchMediaContent();
  }, [])

  if (loading) return <div>Loading media...</div>;

  return (
      <div className="flex-1 bg-white p-4 rounded shadow">
    <h1 className="text-2xl mb-4">Home Page</h1>
    <ul>
      {mediaContent.map((item) => (
          <li key={item.id}>{item.title} - {item.description}</li>
        ))}
    </ul>
  </div>
  );
}