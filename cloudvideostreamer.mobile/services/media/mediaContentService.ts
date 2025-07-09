import MediaContent from "@/models/MediaContent";
import BaseService from "../base/baseService";

class MediaContentService extends BaseService<MediaContent> {
    constructor() {
        super("MediaContent");
    }
}

export const mediaContentService = new MediaContentService();